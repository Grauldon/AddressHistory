using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using AddressHistoryDAL.Models;

namespace AddressHistoryDAL.EF
{
    public class AddressHistoryContext : DbContext
    {
        internal AddressHistoryContext() {}
        public AddressHistoryContext(DbContextOptions options) : base(options) {}

        public DbSet<Address> Address { get; set; }

        public string GetTableName(Type type) => Model.FindEntityType(type).SqlServer().TableName;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = @"server=(LocalDb)\MSSQLLocalDB;database=AddressHistory;integrated security=True;
                    MultipleActiveResultSets=True;App=EntityFramework;";
                optionsBuilder.UseSqlServer(
                    connectionString,
                    options => options.EnableRetryOnFailure())
                    .ConfigureWarnings(warnings=>warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //create the multi column index
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => new { e.StartDate, e.EndDate });
            });
        }
    }
}
