using AutoMapper;
using DivingApp.Models;
using DivingApp.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Common.Resolver
{
    public class DicCountryResolver : ValueResolver<int, DicCountry>
    {

        protected override DicCountry ResolveCore(int source)
        {
            EntityContext _context = new EntityContext();
            return _context.DicCountries.Where(c => c.CountryKod == source).First();
        }
    }
}
