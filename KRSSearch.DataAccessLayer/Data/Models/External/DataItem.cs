using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRSSearch.DataAccessLayer.Data.Models
{
    public class DataItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("dataset")]
        public string Dataset { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("data")]
        public DataDetails Data { get; set; }
    }
}
