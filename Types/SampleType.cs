using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Maui_RavenDemo.Types
{
    public class SampleType
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}
