using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net.Http;

namespace MigracionAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        //aqui la referencia al api que pidio 
        
        private const string NEWS_URL = "https://remolacha.net/wp-json/wp/v2/posts?search=migraci%C3%B3n&_fields=title,excerpt";

        public NewsController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(NEWS_URL);
                var news = JsonSerializer.Deserialize<List<NewsItem>>(response);
                
                var formattedNews = news.Select(n => new {
                    Titulo = n.title.rendered,
                    Resumen = StripHtmlTags(n.excerpt.rendered)
                });

                return Ok(formattedNews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener las noticias: " + ex.Message);
            }
        }

        private string StripHtmlTags(string html)
        {
            return System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", string.Empty);
        }

        private class NewsItem
        {
            public Title title { get; set; }
            public Excerpt excerpt { get; set; }
        }

        private class Title
        {
            public string rendered { get; set; }
        }

        private class Excerpt
        {
            public string rendered { get; set; }
        }
    }
}