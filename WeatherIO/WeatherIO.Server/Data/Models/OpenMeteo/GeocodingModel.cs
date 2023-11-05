using System.Text.Json.Serialization;

namespace WeatherIO.Server.Data.Models.OpenMeteo
{
    public class GeocodingModel
    {
        public class GeocodingResult
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("latitude")]
            public double Latitude { get; set; }

            [JsonPropertyName("longitude")]
            public double Longitude { get; set; }

            [JsonPropertyName("elevation")]
            public double Elevation { get; set; }

            [JsonPropertyName("feature_code")]
            public string FeatureCode { get; set; }

            [JsonPropertyName("country_code")]
            public string CountryCode { get; set; }

            [JsonPropertyName("timezone")]
            public string Timezone { get; set; }

            [JsonPropertyName("population")]
            public int Population { get; set; }
        }

        [JsonPropertyName("results")]
        public IList<GeocodingResult> Results { get; set; }

        [JsonPropertyName("generationtime_ms")]
        public double GenerationTime { get; set; }
    }
}
