using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AddressHistoryDAL.EF
{
    public class AddressHistoryContextFactory : IDesignTimeDbContextFactory<AddressHistoryContext>
    {
        public AddressHistoryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AddressHistoryContext>();
            var connectionString = @"server=(LocalDb)\MSSQLLocalDB;database=AddressHistory;integrated security=True;
                    MultipleActiveResultSets=True;App=EntityFramework;";
            optionsBuilder.UseSqlServer(
                    connectionString,
                    options => options.EnableRetryOnFailure())
                .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            return new AddressHistoryContext(optionsBuilder.Options);
        }
    }
}
