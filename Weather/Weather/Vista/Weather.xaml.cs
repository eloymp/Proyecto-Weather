using Weather.Servicios;
using Weather.Singleton;
using Weather.Modelo;


namespace Weather.Vista;

public partial class Weather : ContentPage
{
    Geolocalizacion geo;
    OpenWeather ObjetoClima;
    private List<Geolocalizacion> geolocalizaciones;

    public Weather()
    {
        InitializeComponent();
        geolocalizaciones = new List<Geolocalizacion>();

    }
    private async void MostrarListaCiudades()//Hay que hacerlo async
    {
        Nominatim ObjetoGeolocalizacion = new Nominatim();
        if (ListaLocalizaciones.ItemsSource != null)
        {
            ListaLocalizaciones.ItemsSource.Clear();
        }

        var ciudades = await ObjetoGeolocalizacion.ObtenerListaCiudades(BarraBusqueda.Text);

        if (ciudades.Count > 0 )
        {
            ListaLocalizaciones.ItemsSource=ciudades;
            ListaLocalizaciones.SelectedIndex = 0;

        }
        else 
        {
            DisplayAlert("Error", "Busqueda incorrecta","Aceptar");
        }

    }
    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        MostrarListaCiudades();
    }

    private async void ListaLocalizaciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjetoClima = new OpenWeather();
        geo = ListaLocalizaciones.SelectedItem as Geolocalizacion;

        string lat = geo.Latitud;
        string lon = geo.Longitud;

        //await DisplayAlert("Información de Geolocalización", $"Latitud: {lat}, Longitud: {lon}", "OK");


        var clima = await ObjetoClima.ObtenerClima(lat, lon);

        
        lblTemperatura.Text = $"{clima.Temperatura} °C";
        lblDescripcion.Text = clima.Descripcion;
        imgIcon.Source = clima.IconoUrl;

    }
}