using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRSSearch.DataAccessLayer.Data.Models
{
    public class RootObject
    {
        [JsonProperty("Dataobject")]
        public DataItem[] DataObjects;     
    }


    
}
