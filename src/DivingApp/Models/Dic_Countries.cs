//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DivingApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dic_Countries
    {
        public Dic_Countries()
        {
            this.Dives = new HashSet<Dives>();
            this.Users = new HashSet<User>();
        }
    
        public decimal dic_val_kod { get; set; }
        public string dic_val_ru { get; set; }
        public string dic_val_ua { get; set; }
        public string fullname { get; set; }
        public string phone_code { get; set; }
        public string smallname { get; set; }
        public string smallname2 { get; set; }
        public byte[] flag { get; set; }
        public Nullable<int> tmp1 { get; set; }
        public Nullable<int> tmp2 { get; set; }
        public Nullable<int> tmp3 { get; set; }
        public string description { get; set; }
    
        public virtual ICollection<Dives> Dives { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
