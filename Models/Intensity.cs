﻿using Newtonsoft.Json;

namespace CarbonIntensity.Models
{
    public class Intensity
    {
        [JsonProperty("forecast")]
        public int? Forecast { get; set; }

        [JsonProperty("actual")]
        public int? Actual { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }
    }
}
