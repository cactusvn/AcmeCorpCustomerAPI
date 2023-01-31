using AcmeCorpCustomerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorpCustomerAPI.Services
{
    public static class DataMigrationHelper
    {
        public static void DoDataMigration(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Migrate(serviceScope.ServiceProvider.GetService<CustomersDbContext>());
            }
        }
        private static void Migrate(CustomersDbContext context)
        {
            System.Console.WriteLine("Appling Migrations...");
            context.Database.Migrate();
            System.Console.WriteLine("Done Migrations...");
        }
    }
}

