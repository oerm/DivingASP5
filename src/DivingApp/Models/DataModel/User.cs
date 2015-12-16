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
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;

    public class User: IdentityUser
    {    
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Nullable<System.DateTime> Birth { get; set; }

        public Nullable<int> Country { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public byte[] Photo { get; set; }

        public bool Status { get; set; }

        public virtual DicCountry DicCountry { get; set; }
        public virtual ICollection<Cert> Certs { get; set; }     
        public virtual ICollection<Dive> Dives { get; set; }
    }
}
