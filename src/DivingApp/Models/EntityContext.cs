using DivingApp.Models.DataModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Models
{
    public class EntityContext : IdentityDbContext<User>
    {
        public EntityContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Trusted_Connection=true;Database=Diving;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            base.OnModelCreating(builder);      
        }

        public DbSet<Cert> Certs { get; set; }

        public DbSet<Dive> Dives { get; set; }

        public DbSet<Photos> Photos { get; set; }

        public DbSet<PhotoImg> PhotoImgSet { get; set; }

        public DbSet<DicCert> DicCerts { get; set; }

        public DbSet<DicCountry> DicCountries { get; set; }

        public DbSet<DicSuit> DicSuitTypes { get; set; }

        public DbSet<DicTank> DicTankTypes { get; set; }

        public DbSet<DicWeightOk> DicWeightOk { get; set; }

        public DbSet<DicDiveTime> DicDiveTime { get; set; }
    }
}
