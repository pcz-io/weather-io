namespace WeatherIO.Common.Data.Models.APIResponses
{
    public class GeocodeResponse
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
