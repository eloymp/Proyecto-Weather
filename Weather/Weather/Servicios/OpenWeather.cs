using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Weather.Modelo;

namespace Weather.Servicios
{
    public class OpenWeather
    {
        public async Task<Clima> ObtenerClima(string latitud, string longitud)
        {
            string respuesta;
            var direccion = new Uri("http://api.openweathermap.org/data/2.5/weather");

            string apiKey = "aqui va tu key";
            string consulta = $"?lat={latitud}&lon={longitud}&appid={apiKey}&units=metric&lang=es";

            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                using (var response = await httpClient.GetAsync(consulta))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Error al realizar la consulta a la API de OpenWeather.");
                    }

                    respuesta = await response.Content.ReadAsStringAsync();
                    var miJSON = JsonDocument.Parse(respuesta);

                    // Crear un objeto Clima con los datos recibidos
                    var clima = new Clima();

                    //Coger la descripcion del clima
                    if (miJSON.RootElement.TryGetProperty("weather", out var weather) &&
                        weather[0].TryGetProperty("description", out var description))
                    {
                        clima.Descripcion = description.GetString();
                    }

                    //Coger la temperatura
                    if (miJSON.RootElement.TryGetProperty("main", out var main) &&
                        main.TryGetProperty("temp", out var temp))
                    {
                        clima.Temperatura = temp.GetDouble().ToString("F1"); //Temperatura con un decimal
                    }

                    //Coger el icono del clima
                    if (miJSON.RootElement.TryGetProperty("weather", out var weatherArray) &&
                        weatherArray[0].TryGetProperty("icon", out var icon))
                    {
                        string iconCode = icon.GetString();
                        clima.IconoUrl = $"http://openweathermap.org/img/wn/{iconCode}@2x.png"; // URL del ícono
                    }

                    return clima;
                }
            }
        }
    }
}


