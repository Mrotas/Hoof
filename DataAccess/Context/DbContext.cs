using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataAccess.Entities;
using DataAccess.Entities.AnnualPlan;
using DataBaseContext = System.Data.Entity.DbContext;

namespace DataAccess.Context
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

        #region AnnualPlan

        public DbSet<CostPlan> CostPlan { get; set; }
        public DbSet<EmployeePlan> EmployeePlan { get; set; }
        public DbSet<EstimatedGameBeforeHuntPeriodPlan> EstimatedGameBeforeHuntPeriodPlan { get; set; }
        public DbSet<EstimatedGameCount30MarchPlan> EstimatedGameCount30MarchPlan { get; set; }
        public DbSet<FieldPlan> FieldPlan { get; set; }
        public DbSet<FodderPlan> FodderPlan { get; set; }
        public DbSet<GameHuntPlan> GamePlan { get; set; }
        public DbSet<HuntEquipmentPlan> HuntEquipmentPlan { get; set; }
        public DbSet<TrunkFoodPlan> TrunkFoodPlan { get; set; }

        #endregion

        public DbSet<Hunt> Hunt { get; set; }
        public DbSet<Huntsman> Huntsman { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<CodeTable> CodeTable { get; set; }
    }
}
