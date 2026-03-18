using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TheITBookOnlineShop.Models.DTOs; 

namespace TheITBookOnlineShop.Controllers
{
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IHttpClientFactory httpClientFactory, ILogger<BooksController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet("/books")]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

                var response = await client.GetAsync("https://www.googleapis.com/books/v1/volumes?q=mysql");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, new { message = "Failed to fetch data from API." });
                }

                var jsonString = await response.Content.ReadAsStringAsync();

                var googleBooksResponse = JsonSerializer.Deserialize<GoogleBooksResponse>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (googleBooksResponse?.Items == null)
                {
                    return NotFound(new { message = "No books found." });
                }

                var sortedBooks = googleBooksResponse.Items
                    .Where(b => b.VolumeInfo != null)
                    .Select(b => new
                    {
                        title = b.VolumeInfo!.Title ?? "Unknown Title",
                        subtitle = b.VolumeInfo!.Subtitle ?? "",
                        authors = b.VolumeInfo!.Authors != null ? string.Join(", ", b.VolumeInfo!.Authors) : "",
                        pageCount = b.VolumeInfo!.PageCount
                    })
                    .OrderBy(b => b.title)
                    .ToList();

                return Ok(sortedBooks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books");
                return StatusCode(500, new
                {
                    message = "Error fetching data",
                    errorDetail = ex.Message
                });
            }
        }
    } 
} 