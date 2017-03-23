using DataAccessLayer;
using KRSSearch.DataAccessLayer;
using KRSSearch.DataAccessLayer.Data.Models;
using KRSSearch.Logic.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KRSSearch.Logic
{
    public class AssociationRepository
    {
        private List<AssociationModel> ConvertJsonDataToModel(string data)
        {
            var objectJson = JsonConvert.DeserializeObject<DataAccessLayer.Data.Models.RootObject>(data);
            var list = new List<AssociationModel>();
            if (objectJson == null)
                return list;

            foreach (var item in objectJson.DataObjects)
            {
                list.Add(new AssociationModel()
                {
                    BaseId = item.Id,
                    Email= item.Data.Email,
                    LegalForm = item.Data.LegalForm,
                    Name = item.Data.Name,
                    RepresentationName = item.Data.RepresentationName,
                    WebSite= item.Data.WebSite,
                    HeadQuarter = item.Data.HeadQuarter
                }); 
            }
            return list;

        }
        public void UpdateDatabaseFromAPI()
        {

            DataService dataService = new DataService();
            if (dataService.GetLastDatabaseUpdateDate().AddDays(5) < DateTime.Now)
            {
                for (int i = 1; i < 200; i++)
                {
                    Uri uri = new Uri("https://api-v3.mojepanstwo.pl/dane/krs_podmioty.json?page=" + i + "&limit=1000&conditions[krs_podmioty.forma_prawna_typ_id]=2");
                    try
                    {
                        var request = (HttpWebRequest)WebRequest.Create(uri);
                        request.Method = "GET";

                        WebResponse response = request.GetResponse();
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                        var list = ConvertJsonDataToModel(responseString);
                        dataService.InsertOnLoad(list);


                    }
                    catch (Exception ex)
                    {

                    }

                }
                dataService.UpdateLastDataDownload();
            }

        }
    }
}
