using KRSSearch.DataAccessLayer.Data;
using KRSSearch.DataAccessLayer.Data.Models;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRSSearch.DataAccessLayer
{
    public class DataService
    {
        const string DataFile = "Database.db";


        public DateTime GetLastDatabaseUpdateDate()
        {
            using (var db = new LiteDatabase(DataFile))
            {
                var databaseUpdates = db.GetCollection<DatabaseUpdatesModel>("DatabaseUpdates");
                var databaseUpdatesList = databaseUpdates.FindAll();
                var lastDate = databaseUpdatesList.LastOrDefault();

                if (lastDate == null)
                    return DateTime.MinValue;



                return lastDate.UpdateDate;
            }
        }

        public void UpdateLastDataDownload()
        {
            using (var db = new LiteDatabase(DataFile))
            {
                var databaseUpdates = db.GetCollection<DatabaseUpdatesModel>("DatabaseUpdates");
                databaseUpdates.Insert(new DatabaseUpdatesModel() { UpdateDate = DateTime.Now});
            }
        }
        public void InsertOnLoad(List<AssociationModel> data)
        {
            using (var db = new LiteDatabase(DataFile))
            {
                var associatons = db.GetCollection<AssociationModel>("associations");

                associatons.Insert(data);
            }
        }
    }
}
