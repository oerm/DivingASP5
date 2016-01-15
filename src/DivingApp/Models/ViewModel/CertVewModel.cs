using System;
using System.Collections.Generic;


namespace DivingApp.Models.ViewModel
{

    public class CertViewModel
    {
        public string UserID { get; set; }     

        public Nullable<System.DateTime> DateArchieve { get; set; }

        public string CertNumber { get; set; }

        public string Issuer { get; set; }

        public byte[] Photo { get; set; }

        public decimal CertID { get; set; }

        public string CertName { get; set; }

        public byte Level { get; set; }

        public bool IsGeneral { get; set; }

        public string Description { get; set; }
    }

}