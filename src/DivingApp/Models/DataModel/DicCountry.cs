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

    public class DicCountry
    {    
        [Key]
        public int CountryKod { get; set; }

        public string ValueRU { get; set; }

        public string ValueEU { get; set; }

        public string FullName { get; set; }

        public string PhoneCode { get; set; }

        public string ShortName { get; set; }

        public string ShortName2 { get; set; }

        public byte[] Flag { get; set; }

        public Nullable<int> Tmp1 { get; set; }

        public Nullable<int> Tmp2 { get; set; }

        public Nullable<int> Tmp3 { get; set; }

        public string Description { get; set; }

    }
}
