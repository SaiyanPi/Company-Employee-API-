
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace CompanyEmployees.ContextFactory
{
    //Since Since our RepositoryContext class is in a Repository project and not in the main one
    //This class will help our application create a derived DbContext instance
    //during design time which will help us with our migration
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        // Design time
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseSqlServer(configuration.GetConnectionString("sqlConnection"), 
            b => b.MigrationsAssembly("CompanyEmployees"));
            return new RepositoryContext(builder.Options);
        }
    }
}


