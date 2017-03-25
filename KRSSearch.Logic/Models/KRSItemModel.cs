using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRSSearch.Logic.Models
{
    public class KRSItemModel
    {
        public int Id { get; set; }
        public string RepresentationName { get; set; }
        public string LegalForm { get; set; }
        public string HeadQuarter { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Regon { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Country { get; set; }
        public int VoivodeShipId { get; set; }
    }
    public enum EmailStatus
    {
        [Description("Wszystkie")]
        All = 0,
        [Description("Tylko z adresem email")]
        WithEmail = 1,
        [Description("Tylko bez adresu email")]
        WithoutEmail = 2,
    }
    public enum WebSiteStatus
    {
        [Description("Wszystkie")]
        All = 0,
        [Description("Tylko ze adresem www")]
        WithWWW = 1,
        [Description("Tylko bez adresu www")]
        WithoutWWW = 2,
    }
    public enum FilterTypes
    {
        HeadQuarters=0,
        LegalForms,
        RepresentationName
    }
}
