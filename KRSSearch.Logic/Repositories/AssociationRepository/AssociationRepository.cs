using DataAccessLayer;
using KRSSearch.DataAccessLayer;
using KRSSearch.DataAccessLayer.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using KRSSearch.Logic.Models;

namespace KRSSearch.Logic
{
    public class AssociationRepository : IAssociationRepository
    {
        private DataService _dataService;

        public AssociationRepository(DataService dataService)
        {
            _dataService = dataService;
        }
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
            if (_dataService.GetLastDatabaseUpdateDate().AddDays(5) < DateTime.Now)
            {
                _dataService.DeleteDataOnLoad();
                for (int i = 1; i < 265; i++)
                {
                    Uri uri = new Uri("https://api-v3.mojepanstwo.pl/dane/krs_podmioty.json?page=" + i + "&limit=1000&conditions[krs_podmioty.forma_prawna_typ_id]=2");
                    try
                    {
                        var request = (HttpWebRequest)WebRequest.Create(uri);
                        request.Method = "GET";

                        WebResponse response = request.GetResponse();
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                        var list = ConvertJsonDataToModel(responseString);
                        _dataService.InsertOnLoad(list);


                    }
                    catch (Exception ex)
                    {

                    }

                }
                _dataService.UpdateLastDataDownload();
            }

        }

        public List<KRSItemModel> GetData()
        {
            var data = _dataService.GetData();
            List<KRSItemModel> list = new List<KRSItemModel>();
            foreach(var item in data)
            {
                list.Add(new KRSItemModel()
                {
                    Id = item.Id,
                    Email = item.Email,
                    Name = item.Name,
                    LegalForm = item.LegalForm,
                    HeadQuarter = item.HeadQuarter,
                    RepresentationName = item.RepresentationName,
                    WebSite=item.WebSite,
                    Country = item.Country,
                    Regon = item.Regon,
                    RegistrationDate = item.RegistrationDate,
                    VoivodeShipId = item.VoivodeShipId
                });      
            }
            return list;
            
        }

        public List<string> Get(FilterTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
