using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Models.Request
{
    public class ServerResult<TResult>
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public int Next { get; set; }

        [JsonProperty("previous")]
        public int Previous { get; set; }

        [JsonProperty("results")]
        public TResult[] Results { get; set; }
    }
}
