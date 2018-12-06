﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataAccess.Entities;
using DatabaseContext = System.Data.Entity.DbContext;

namespace DataAccess.Context
{
    public class DbContext : DatabaseContext
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
        
        public DbSet<CodeTable> CodeTable { get; set; }
        public DbSet<CostPlan> CostPlan { get; set; }
        public DbSet<EmployeePlan> EmployeePlan { get; set; }
        public DbSet<FieldPlan> FieldPlan { get; set; }
        public DbSet<FodderPlan> FodderPlan { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<GameCountBefore10March> GameCountBefore10MarchPlan { get; set; }
        public DbSet<GameCountBeforeHuntingSeasonPlan> GameCountBeforeHuntingSeason { get; set; }
        public DbSet<GameHuntPlan> GameHuntPlan { get; set; }
        public DbSet<GameLoss> GameLoss { get; set; }
        public DbSet<GameSettlementCountBefore10March> GameSettlementCountBefore10March { get; set; }
        public DbSet<GameSettlementCountPlan> GameSettlementPlanCount { get; set; }
        public DbSet<Hunt> Hunt { get; set; }
        public DbSet<HuntEquipmentPlan> HuntEquipmentPlan { get; set; }
        public DbSet<Huntsman> Huntsman { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<TrunkFoodPlan> TrunkFoodPlan { get; set; }
    }
}
