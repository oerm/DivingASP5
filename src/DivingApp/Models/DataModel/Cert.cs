//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DivingApp.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cert
    {      
        public Nullable<System.DateTime> DateArchieve { get; set; }
        [Key]
        public string CertNumber { get; set; }
        public string Issuer { get; set; }
        public byte[] Photo { get; set; }
    
        public virtual User User { get; set; }
        public virtual DicCert DicCert { get; set; }
    }
}
