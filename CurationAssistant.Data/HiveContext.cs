using CurationAssistant.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurationAssistant.Data
{
    class HiveContext : DbContext
    {
        public HiveContext() : base(nameOrConnectionString: "Default") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Block> Blocks { get; set; }

    }
}
