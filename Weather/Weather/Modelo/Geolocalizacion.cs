namespace Weather.Modelo
{
    public class Geolocalizacion
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Longitud { get; set; }
        public string Latitud { get; set; }
        

        public Geolocalizacion(string n, string t, string lat, string lon)
        {
            this.Nombre = n;
            this.Tipo = t;
            this.Longitud = lon;
            this.Latitud = lat;
            
        }

        public override string ToString()
        {
            return $"{Nombre} ({Tipo})";
        }
    }
}
