namespace WeatherIO.Server.Data.Models.OpenMeteo
{
    /// <summary>
    /// This class represents data returned from OpenMeteo Air Quality API
    /// </summary>
    public class AirQualityModel
    {
        /// <summary>
        /// Represents JSON object with current air quality data.
        /// </summary>
        public class CurrentData
        {
            /// <summary>
            /// Time of the response
            /// </summary>
            public DateTime time { get; set; }

            /// <summary>
            /// European Air Quality Index
            /// </summary>
            public int european_aqi { get; set; }

            /// <summary>
            /// Particulate matter with diameter smaller than 10µm in µg/m3
            /// </summary>
            public double pm10 { get; set; }

            /// <summary>
            /// Particulate matter with diameter smaller than 2.5µm in µg/m3
            /// </summary>
            public double pm2_5 { get; set; }

            /// <summary>
            /// Concentration of carbon dioxide in µg/m3
            /// </summary>
            public double carbon_monoxide { get; set; }

            /// <summary>
            /// Concentration of nitrogen dioxide in µg/m3
            /// </summary>
            public double nitrogen_dioxide { get; set; }

            /// <summary>
            /// Concentration of sulphur dioxide in µg/m3
            /// </summary>
            public double sulphur_dioxide { get; set; }

            /// <summary>
            /// Concentration of ozone in µg/m3
            /// </summary>
            public double ozone { get; set; }
        }

        /// <summary>
        /// Latitude of the location
        /// </summary>
        public double latitude { get; set; }

        /// <summary>
        /// Longitude of the location
        /// </summary>
        public double longitude { get; set; }

        /// <summary>
        /// Elevation in meters
        /// </summary>
        public double elevation { get; set; }

        /// <summary>
        /// Object with current air quality data
        /// </summary>
        public CurrentData current { get; set; }
    }
}
