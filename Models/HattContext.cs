using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Hattmakarens_system.Models
{
    public class HattContext: DbContext
    {
        public DbSet<AnvandareModell> User;
        public DbSet<BestallningModell> Order;
        public DbSet<KundModell> Customer;
        public DbSet<Hatt> Hatt;
        public DbSet<HattModeller> HattModeller;
        public DbSet<BildModell> Pictures;
        public DbSet<MaterialModell> Material;
        public DbSet<FargModell> Color;

        public HattContext() : base()
        {

        }
    }
}