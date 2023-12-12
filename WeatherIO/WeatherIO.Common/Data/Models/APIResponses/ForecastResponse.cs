namespace WeatherIO.Common.Data.Models.APIResponses
{
    /// <summary>
    /// Represents response data from `get-forecast` and `get-forecast-by-city` endpoints
    /// </summary>
    public class ForecastResponse
    {
        /// <summary>
        /// Represents one data sample in ForecastResponse
        /// </summary>
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

        /// <summary>
        /// Represents current weather data
        /// </summary>
        public ForecastTimePoint Current { get; set; }

        /// <summary>
        /// Represents forecast data
        /// </summary>
        public IList<ForecastTimePoint> Forecast { get; set; }
    }
}
