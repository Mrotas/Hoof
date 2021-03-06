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
        
        public DbSet<AnnualPlanStatus> AnnualPlanStatus { get; set; }
        public DbSet<CarcassRevenue> CarcassRevenue { get; set; }
        public DbSet<Catch> Catch { get; set; }
        public DbSet<CostPlan> CostPlan { get; set; }
        public DbSet<DeerLicker> DeerLicker { get; set; }
        public DbSet<EmploymentPlan> EmploymentPlan { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<FieldPlan> FieldPlan { get; set; }
        public DbSet<Fodder> Fodder { get; set; }
        public DbSet<FodderPlan> FodderPlan { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<GameClass> GameClass { get; set; }
        public DbSet<GameCountFor10March> GameCountFor10March { get; set; }
        public DbSet<GameHuntPlan> GameHuntPlan { get; set; }
        public DbSet<Hunt> Hunt { get; set; }
        public DbSet<HuntedGame> HuntedGame { get; set; }
        public DbSet<HuntEquipmentPlan> HuntEquipmentPlan { get; set; }
        public DbSet<Huntsman> Huntsman { get; set; }
        public DbSet<Labor> Labor { get; set; }
        public DbSet<Loss> Loss { get; set; }
        public DbSet<LossGame> LossGame { get; set; }
        public DbSet<MarketingYear> MarketingYear { get; set; }
        public DbSet<Pasture> Pasture { get; set; }
        public DbSet<Pulpit> Pulpit { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<TrunkFoodPlan> TrunkFoodPlan { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<WateringPlace> WateringPlace { get; set; }
    }
}
