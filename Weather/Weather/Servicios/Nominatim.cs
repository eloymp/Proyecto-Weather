using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Weather.Modelo;

namespace Weather.Servicios
{
    public class Nominatim
    {
        public async Task<List<Geolocalizacion>> ObtenerListaCiudades(string ciudadABuscar)
        {
            Geolocalizacion loc;
            JsonDocument miJSON;
            string respuesta, name, displayName, latitude, longitude;
            List<Geolocalizacion> resultado = new List<Geolocalizacion>();
            var direccion = new Uri("https://nominatim.openstreetmap.org/");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Weather/1.0");
                string consulta = $"search?q={ciudadABuscar}&format=json&HTTP=referer";
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    miJSON = JsonDocument.Parse(respuesta);
                    int numCiudades = miJSON.RootElement.GetArrayLength();
                    for (int i = 0; i < numCiudades; i++)
                    {
                        string[] address_types = { "town", "city", "village", "municipality" };
                        if (address_types.Contains(miJSON.RootElement[i].GetProperty("addresstype").ToString()))
                        {
                            name = miJSON.RootElement[i].GetProperty("name").ToString();
                            displayName = miJSON.RootElement[i].GetProperty("display_name").ToString();
                            latitude = miJSON.RootElement[i].GetProperty("lat").ToString();
                            longitude = miJSON.RootElement[i].GetProperty("lon").ToString();
                            loc = new Geolocalizacion(name, displayName, latitude, longitude);
                            resultado.Add(loc);
                        }
                    }
                }
            }
            return resultado;
        }
    }
}
