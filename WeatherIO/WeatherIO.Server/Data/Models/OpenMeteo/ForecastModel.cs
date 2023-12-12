using WeatherIO.Common.Data.Models.APIResponses;
using static WeatherIO.Common.Data.Models.APIResponses.ForecastResponse;

namespace WeatherIO.Server.Data.Models.OpenMeteo
{
    /// <summary>
    /// This class represents OpenMeteo Forecast API response
    /// </summary>
    public class ForecastModel
    {
        /// <summary>
        /// This enum contains WMO codes
        /// </summary>
        public enum WeatherCode
        {
            ClearSky = 0,

            MainlyClear = 1,
            PartialyCloudy = 2,
            Overcast = 3,

            Fog = 45,
            DepositingRimeFog = 48,

            LightDrizzle = 51,
            ModerateDrizzle = 53,
            DenseDrizzle = 55,

            LightFreezingDrizzle = 56,
            DenseFreezingDrizzle = 57,

            SlightRain = 61,
            ModerateRain = 63,
            HeavyRain = 65,

            LightFreezingRain = 66,
            HeavyFreezingRain = 67,

            SlightSnowFall = 71,
            ModerateSnowFall = 73,
            HeavySnowFall = 75,

            SnowGrains = 77,

            SlightRainShowers = 80,
            ModerateRainShowers = 81,
            ViolentRainShowers = 82,

            SlightSnow = 85,
            HeavySnow = 86,

            Thunderstorm = 95,

            ThunderStormWithSlightHail = 96,
            ThunderStormWithHeavyHail = 99
        }

        /// <summary>
        /// This class represents JSON object for current weather data
        /// </summary>
        public class CurrentData
        {
            /// <summary>
            /// Time of the response
            /// </summary>
            public DateTime time { get; set; }

            /// <summary>
            /// Temperature 2 meters above surface
            /// </summary>
            public double? temperature_2m { get; set; }

            /// <summary>
            /// Relative humidity 2 meters above surface
            /// </summary>
            public double? relative_humidity_2m { get; set; }

            /// <summary>
            /// Apparent temperature
            /// </summary>
            public double? apparent_temperature { get; set; }

            /// <summary>
            /// Precipitation in mm
            /// </summary>
            public double? precipitation { get; set; }

            /// <summary>
            /// Rain in mm
            /// </summary>
            public double? rain { get; set; }

            /// <summary>
            /// Showers in mm
            /// </summary>
            public double? showers { get; set; }

            /// <summary>
            /// Snowfall in mm
            /// </summary>
            public double? snowfall { get; set; }

            /// <summary>
            /// WMO code for this time
            /// </summary>
            public WeatherCode? weathercode { get; set; }

            /// <summary>
            /// Cloud cover in percents
            /// </summary>
            public double? cloudcover { get; set; }

            /// <summary>
            /// Surface pressure in hPa
            /// </summary>
            public double? surface_pressure { get; set; }

            /// <summary>
            /// Wind speed 10 meters above surface in km/h
            /// </summary>
            public double? wind_speed_10m { get; set; }

            /// <summary>
            /// Wind direction in degrees
            /// </summary>
            public double? wind_direction_10m { get; set; }

            /// <summary>
            /// Wind gusts 10 meters above surface in km/h
            /// </summary>
            public double? wind_gusts_10m { get; set; }
        }

        public class ForecastData
        {
            /// <summary>
            /// Time of the response
            /// </summary>
            public IList<DateTime> time { get; set; }

            /// <summary>
            /// Temperature 2 meters above surface
            /// </summary>
            public IList<double>? temperature_2m { get; set; }

            /// <summary>
            /// Relative humidity 2 meters above surface
            /// </summary>
            public IList<double>? relative_humidity_2m { get; set; }

            /// <summary>
            /// Apparent temperature
            /// </summary>
            public IList<double>? apparent_temperature { get; set; }

            /// <summary>
            /// Precipitation in mm
            /// </summary>
            public IList<double>? precipitation { get; set; }

            /// <summary>
            /// Rain in mm
            /// </summary>
            public IList<double>? rain { get; set; }

            /// <summary>
            /// Showers in mm
            /// </summary>
            public IList<double>? showers { get; set; }

            /// <summary>
            /// Snowfall in mm
            /// </summary>
            public IList<double>? snowfall { get; set; }

            /// <summary>
            /// WMO code for this time
            /// </summary>
            public IList<WeatherCode>? weathercode { get; set; }

            /// <summary>
            /// Cloud cover in percents
            /// </summary>
            public IList<double>? cloudcover { get; set; }

            /// <summary>
            /// Surface pressure in hPa
            /// </summary>
            public IList<double>? surface_pressure { get; set; }

            /// <summary>
            /// Wind speed 10 meters above surface in km/h
            /// </summary>
            public IList<double>? wind_speed_10m { get; set; }

            /// <summary>
            /// Wind direction in degrees
            /// </summary>
            public IList<double>? wind_direction_10m { get; set; }

            /// <summary>
            /// Wind gusts 10 meters above surface in km/h
            /// </summary>
            public IList<double>? wind_gusts_10m { get; set; }
        }

        /// <summary>
        /// Latitude of the forecast location
        /// </summary>
        public double latitude { get; set; }

        /// <summary>
        /// Longitude of the forecast location
        /// </summary>
        public double longitude { get; set; }

        /// <summary>
        /// Object with current weather data
        /// </summary>
        public CurrentData current { get; set; }

        /// <summary>
        /// Object with forecast data
        /// </summary>
        public ForecastData hourly { get; set; }

        /// <summary>
        /// This method converts object into ForecastResponse
        /// </summary>
        /// <returns>ForecastResponse</returns>
        public ForecastResponse ToResponse()
        {
            return new() {
                Current = new()
                {
                    Time = this.current.time,
                    WMOCode = this.current.weathercode == null ? null : (int)this.current.weathercode,
                    WMOString = this.current.weathercode == null ? null : MapWMOCodeToString(this.current.weathercode.Value),
                    Temperature = this.current.temperature_2m,
                    ApparentTemperature = this.current.apparent_temperature,
                    Precipitation = this.current.precipitation,
                    Humidity = this.current.relative_humidity_2m,
                    Pressure = this.current.surface_pressure,
                    WindSpeed = this.current.wind_speed_10m,
                    WindDirection = this.current.wind_direction_10m,
                    WindGusts = this.current.wind_gusts_10m
                },
                Forecast = GetForecastTimePoints()
            };
        }

        /// <summary>
        /// Private helper method that creates list of the ForecastTimePoint from hourly data
        /// </summary>
        /// <returns>List of ForecastTimePoint</returns>
        private List<ForecastTimePoint> GetForecastTimePoints()
        {
            T? ForwardNullElement<T>(IList<T>? value, int index) where T : struct
                => value == null ? null : value[index];

            var result = new List<ForecastTimePoint>();
            for (int i = 0; i < this.hourly.time.Count; ++i)
            {
                result.Add(new ForecastTimePoint
                {
                    Time = this.hourly.time[i],
                    WMOCode = this.hourly.weathercode != null ? (int)this.hourly.weathercode[i] : null,
                    WMOString = this.hourly.weathercode != null ? MapWMOCodeToString(this.hourly.weathercode[i]) : null,
                    Temperature = ForwardNullElement(this.hourly.temperature_2m, i),
                    ApparentTemperature = ForwardNullElement(this.hourly.apparent_temperature, i),
                    Precipitation = ForwardNullElement(this.hourly.precipitation, i),
                    Humidity = ForwardNullElement(this.hourly.relative_humidity_2m, i),
                    Pressure = ForwardNullElement(this.hourly.surface_pressure, i),
                    WindSpeed = ForwardNullElement(this.hourly.wind_speed_10m, i),
                    WindDirection = ForwardNullElement(this.hourly.wind_direction_10m, i),
                    WindGusts = ForwardNullElement(this.hourly.wind_gusts_10m, i)
                });
            }
            return result;
        }

        /// <summary>
        /// Converts enum WeatherCode to string in polish
        /// </summary>
        /// <param name="wmoCode">WMO code</param>
        /// <returns>WMO code text message</returns>
        private static string MapWMOCodeToString(WeatherCode wmoCode)
        {
            return wmoCode switch
            {
                WeatherCode.ClearSky => "Bezchmurnie",
                WeatherCode.MainlyClear => "Głównie czyste niebo",
                WeatherCode.PartialyCloudy => "Częściowe zachmurzenie",
                WeatherCode.Overcast => "Pochmurno",
                WeatherCode.Fog => "Mgła",
                WeatherCode.DepositingRimeFog => "Mgła osadzająca szadź",
                WeatherCode.LightDrizzle => "Mżawka",
                WeatherCode.ModerateDrizzle => "Mżawka",
                WeatherCode.DenseDrizzle => "Mżawka",
                WeatherCode.LightFreezingDrizzle => "Marznąca mżawka",
                WeatherCode.DenseFreezingDrizzle => "Marznąca mżawka",
                WeatherCode.SlightRain => "Lekkie opady deszczu",
                WeatherCode.ModerateRain => "Umiarkowane opady deszczu",
                WeatherCode.HeavyRain => "Silne opady deszczu",
                WeatherCode.LightFreezingRain => "Lekkie opady marznącego deszczu",
                WeatherCode.HeavyFreezingRain => "Silne opady marznącego deszczu",
                WeatherCode.SlightSnowFall => "Lekkie opady śniegu",
                WeatherCode.ModerateSnowFall => "Umiarkowane opady śniegu",
                WeatherCode.HeavySnowFall => "Obfite opady śniegu",
                WeatherCode.SnowGrains => "Opady ziarnistego śniegu",
                WeatherCode.SlightRainShowers => "Lekkie przelotne opady deszczu",
                WeatherCode.ModerateRainShowers => "Przelotne opady deszczu",
                WeatherCode.ViolentRainShowers => "Gwałtowne opady deszczu",
                WeatherCode.SlightSnow => "Lekkie opady śniegu",
                WeatherCode.HeavySnow => "Duże opady śniegu",
                WeatherCode.Thunderstorm => "Burza",
                WeatherCode.ThunderStormWithSlightHail => "Burza z gradem",
                WeatherCode.ThunderStormWithHeavyHail => "Burza z silnymi opadami gradu",

                _ => "Nieznany kod WMO"
            };
        }
    }
}
