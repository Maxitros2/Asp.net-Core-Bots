using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkBot5.Models
{
    [Serializable]
    public class IncomeMessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("object")]
        public string Object { get; set; }
        [JsonProperty("group_id")]
        public string GroupId { get; set; }
    }
}
