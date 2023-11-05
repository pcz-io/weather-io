namespace WeatherIO.Server.Data.Models.OpenMeteo
{
    public class AirQualityModel
    {
        public class CurrentData
        {
            public DateTime time { get; set; }
            public int interval { get; set; }
            public int european_aqi { get; set; }
            public double pm10 { get; set; }
            public double pm2_5 { get; set; }
            public double carbon_monoxide { get; set; }
            public double nitrogen_dioxide { get; set; }
            public double sulphur_dioxide { get; set; }
            public double ozone { get; set; }
        }

        public double latitude { get; set; }
        public double longitude { get; set; }
        public double elevation { get; set; }
        public CurrentData current { get; set; }
    }
}
