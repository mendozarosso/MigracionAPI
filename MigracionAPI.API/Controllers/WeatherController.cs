using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace MigracionAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase

    /// <summary>
    /// Obtiene el pronóstico del clima para una ubicación específica
    /// </summary>
    ///  By Jason Mendoza 20231889
    /// <param name="latitud">Latitud de la ubicación</param>
    /// <param name="longitud">Longitud de la ubicación</param>
    /// <returns>Pronóstico del clima</returns>
    /// <response code="200">Pronóstico obtenido exitosamente</response>
    /// <response code="500">Error al obtener el pronóstico</response>
    {
        private readonly HttpClient _httpClient;
        private const string API_KEY = "NotengoApi"; // Tengo que registrarme para esto asi que no agrege el api aqui
        private const string WEATHER_URL = "https://api.openweathermap.org/data/2.5/forecast";

        public WeatherController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] decimal latitud, [FromQuery] decimal longitud)
        {
            try
            {
                var url = $"{WEATHER_URL}?lat={latitud}&lon={longitud}&appid={API_KEY}&units=metric&lang=es";
                var response = await _httpClient.GetStringAsync(url);
                
                // Aquí procesarías la respuesta de la API del clima
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener el clima: " + ex.Message);
            }
        }
    }
}