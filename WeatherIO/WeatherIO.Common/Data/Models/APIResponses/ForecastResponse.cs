namespace WeatherIO.Common.Data.Models.APIResponses
{
    public class ForecastResponse
    {
        public class ForecastTimePoint
        {
            public DateTime Time { get; set; }
            public int? WMOCode { get; set; }
            public string? WMOString { get; set; }
            public double? Temperature { get; set; }
            public double? ApparentTemperature { get; set; }
            public double? Precipitation { get; set; }
            public double? Humidity { get; set; }
            public double? Pressure { get; set; }
            public double? WindSpeed { get; set; }
            public double? WindDirection { get; set; }
            public double? WindGusts { get; set; }
        }

        public ForecastTimePoint Current { get; set; }
        public IList<ForecastTimePoint> Forecast { get; set; }
    }
}
