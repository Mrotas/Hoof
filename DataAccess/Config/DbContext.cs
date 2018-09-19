using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataAccess.Entities;
using DataBaseContext = System.Data.Entity.DbContext;

namespace DataAccess.Config
{
    public class DbContext : DataBaseContext
    {
        public DbContext() : base("name=DbContext")
        {
            Database.SetInitializer<DbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Hunt> Hunt { get; set; }
        public DbSet<Huntsman> Huntsman { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<CodeTable> CodeTable { get; set; }
    }
}
