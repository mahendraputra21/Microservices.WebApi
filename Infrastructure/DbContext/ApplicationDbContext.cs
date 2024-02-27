using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        private readonly IHostEnvironment env;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IConfiguration configuration,
            IHostEnvironment env)
            : base(options)
        {
            this.configuration = configuration;
            this.env = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            string connectionString = "";
            if (env.IsProduction())
            {
                connectionString = configuration.GetConnectionString(Common.Constants.Constant.CONNECTION_STRING_NAME);
            }
            else
            {
                IConfigurationRoot configuration = builder.Build();
                connectionString = configuration.GetConnectionString(Common.Constants.Constant.CONNECTION_STRING_NAME);

            }
            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.CommandTimeout(3600); //  1 hours
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
