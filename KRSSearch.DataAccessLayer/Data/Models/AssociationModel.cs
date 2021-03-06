﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRSSearch.DataAccessLayer.Data.Models
{
    public class AssociationModel
    {
        public int Id { get; set; }
        public int BaseId { get; set; }
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
}
