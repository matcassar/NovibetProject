using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPStackDLL.Models
{
    public class IpResponse : IPDetails
    {
        [JsonProperty("ip")]
        public string ip { get; set; }

        [JsonProperty("continent_code")]
        public string continent_code { get; set; }

        [JsonProperty("continent_name")]
        public string continent_name { get; set; }

        [JsonProperty("country_code")]
        public string country_code { get; set; }

        [JsonProperty("country_name")]
        public string country_name { get; set; }

        [JsonProperty("region_code")]
        public int region_code { get; set; }

        [JsonProperty("region_name")]
        public string region_name { get; set; }

        [JsonProperty("zip")]
        public string zip { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("success")]
        public string Success { get; set; }

        public Guid ipGuid { get; set; }

    }
}