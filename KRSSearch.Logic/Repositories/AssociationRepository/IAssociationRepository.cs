using KRSSearch.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRSSearch.Logic
{
    public interface IAssociationRepository
    {
        void UpdateDatabaseFromAPI();
        List<KRSItemModel> GetData();
        List<string> Get(FilterTypes type);
    }
}
