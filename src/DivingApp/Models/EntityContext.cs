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
            optionsBuilder.UseSqlServer(@"Data Source=.;Integrated security=true;Initial Catalog=Diving");
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Certs> Certs { get; set; }
        public DbSet<Dic_Certs> Dic_Certs { get; set; }
        public DbSet<Dic_Countries> Dic_Countries { get; set; }
        public DbSet<Dic_Suit> Dic_Suit { get; set; }
        public DbSet<Dic_Tank> Dic_Tank { get; set; }
        public DbSet<Dic_WeightOk> Dic_WeightOk { get; set; }
        public DbSet<Dives> Dives { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<PhotoImg> PhotoImgSet { get; set; }
    }
}
