﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRSSearch.Logic.Data.Models
{
    public class DataDetails
    {

        [JsonProperty("krs_podmioty.nazwa_organu_reprezentacji")]
        public string RepresentationName { get; set; }
        [JsonProperty("krs_podmioty.forma_prawna_str")]
        public string LegalForm { get; set; }
        [JsonProperty("krs_podmioty.siedziba")]
        public string HeadQuarter { get; set; }
        [JsonProperty("krs_podmioty.firma")]
        public string Name { get; set; }
        [JsonProperty("krs_podmioty.email")]
        public string Email { get; set; }
        [JsonProperty("krs_podmioty.www")]
        public string WebSite { get; set; }
    }
}
