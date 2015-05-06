using ProjectX.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Data
{
    public class PXContext : DbContext
    {
        public PXContext()
        {
            Database.SetInitializer<PXContext>(new PXInitializer());
        }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailGroup> EmailGroups { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
