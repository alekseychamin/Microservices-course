using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (context.Platforms.Any()) return;
                SeedData(context);
            }
        }

        private static void SeedData(AppDbContext context)
        {
            Console.WriteLine("--> Seeding Data...");

            context.Platforms.AddRange(
                new Platform() {Name = "Dot Net", Publisher = "Microsoft", Cost = "Free"},
                new Platform() {Name="SQL Server Express", Publisher="Microsoft", Cost="Free"},
                new Platform() {Name="Kubernetes", Publisher="Cloud Native Computing Foundation", Cost="Free"}
            );

            context.SaveChanges();
        }
    }
}