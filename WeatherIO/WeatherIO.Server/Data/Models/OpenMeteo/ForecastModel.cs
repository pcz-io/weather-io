using WeatherIO.Common.Data.Models.APIResponses;
using static WeatherIO.Common.Data.Models.APIResponses.ForecastResponse;

namespace WeatherIO.Server.Data.Models.OpenMeteo
{
    public class ForecastModel
    {
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

        public class CurrentData
        {
            // Aktualny czas
            public DateTime time { get; set; }

            // Temperatura na wysokości 2m
            public double temperature_2m { get; set; }

            // Wilgotność w procentach
            public double relative_humidity_2m { get; set; }

            // Temperatura odczuwalna
            public double apparent_temperature { get; set; }

            // Opady w mm
            public double precipitation { get; set; }

            // Deszcz w mm
            public double rain { get; set; }

            // Mżawka w mm
            public double showers { get; set; }

            // Opady śniegu w mm
            public double snowfall { get; set; }

            // Kod WMO
            public WeatherCode weathercode { get; set; }

            // Zachmurzenie w procentach
            public double cloudcover { get; set; }

            // Ciśnienie na powierzchni w hPa
            public double surface_pressure { get; set; }

            // Prędkość wiatru w km/h
            public double wind_speed_10m { get; set; }

            // Kierunek wiatru w stopniach
            public double wind_direction_10m { get; set; }

            // Porywy wiatru w km/h
            public double wind_gusts_10m { get; set; }
        }

        public class ForecastData
        {
            // Aktualny czas
            public IList<DateTime> time { get; set; }

            // Temperatura na wysokości 2m
            public IList<double> temperature_2m { get; set; }

            // 
            public IList<double> relative_humidity_2m { get; set; }

            // Temperatura odczuwalna
            public IList<double> apparent_temperature { get; set; }

            // Opady w mm
            public IList<double> precipitation { get; set; }

            // Deszcz w mm
            public IList<double> rain { get; set; }

            // Mżawka w mm
            public IList<double> showers { get; set; }

            // Opady śniegu w mm
            public IList<double> snowfall { get; set; }

            // Kod WMO
            public IList<WeatherCode> weathercode { get; set; }

            // Zasłona chmurna XD w procentach
            public IList<double> cloudcover { get; set; }

            // Ciśnienie na powierzchni w hPa
            public IList<double> surface_pressure { get; set; }

            // Prędkość wiatru w km/h
            public IList<double> wind_speed_10m { get; set; }

            // Kierunek wiatru w stopniach
            public IList<double> wind_direction_10m { get; set; }

            // Porywy wiatru w km/h
            public IList<double> wind_gusts_10m { get; set; }
        }

        // Szerokość geograficzna
        public double latitude { get; set; }

        // Długość geograficzna
        public double longitude { get; set; }

        public CurrentData current { get; set; }

        public ForecastData hourly { get; set; }

        public ForecastResponse ToResponse()
        {
            return new() {
                Current = new()
                {
                    Time = this.current.time,
                    WMOCode = (int)this.current.weathercode,
                    WMOString = MapWMOCodeToString(this.current.weathercode),
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

        private List<ForecastTimePoint> GetForecastTimePoints()
        {
            var result = new List<ForecastTimePoint>();
            for (int i = 0; i < this.hourly.time.Count; ++i)
            {
                result.Add(new ForecastTimePoint
                {
                    Time = this.hourly.time[i],
                    WMOCode = (int)this.hourly.weathercode[i],
                    WMOString = MapWMOCodeToString(this.hourly.weathercode[i]),
                    Temperature = this.hourly.temperature_2m[i],
                    ApparentTemperature = this.hourly.apparent_temperature[i],
                    Precipitation = this.hourly.precipitation[i],
                    Humidity = this.hourly.relative_humidity_2m[i],
                    Pressure = this.hourly.surface_pressure[i],
                    WindSpeed = this.hourly.wind_speed_10m[i],
                    WindDirection = this.hourly.wind_direction_10m[i],
                    WindGusts = this.hourly.wind_gusts_10m[i]
                });
            }
            return result;
        }

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
