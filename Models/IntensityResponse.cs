using Newtonsoft.Json;
using System;

namespace CarbonIntensity.Models
{
    public class IntensityResponse
    {
        [JsonProperty("from")]
        public DateTime From { get; set; }
        
        [JsonProperty("to")]
        public DateTime To { get; set; }
        
        [JsonProperty("intensity")]
        public Intensity Intensity { get; set; }

        public override string ToString()
        {
            return $"{From} - {To}";
        }
    }        
}