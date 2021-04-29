using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using HCatalyst.Models;

namespace HCatalyst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var oHost = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(oHost);
            oHost.Run();
        }

        /// <summary>
        /// Creates the DB for the application if it has not been done already.
        /// </summary>
        /// <param name="oHost">The Host object for the application.</param>
        private static void CreateDbIfNotExists(IHost oHost)
        {
            using (var scope = oHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<HCcontext>();
                    SeedData(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        /// <summary>
        /// Creates the initial records to populate the list for the application.
        /// </summary>
        /// <param name="context"></param>
        private static void SeedData(HCcontext context)
        {
            context.Database.EnsureCreated();

            // Look for any data.
            if (context.HCpersons.Any())
            {
                return;   // DB has been seeded already
            }

            // The array of data to be used for the seeding.
            HCperson[] hcPersons =
            {
                new HCperson{FirstName="Larry",LastName="Wilson", Street="123 Main St", City="New York", State="NY", Zip="10001", Age=30, Interests="Painting, Reading"},
                new HCperson{FirstName="Henry",LastName="Smith", Street="456 Broadway Ave", City="New York", State="NY", Zip="10002", Age=41, Interests="Golf, Reading"},
                new HCperson{FirstName="Mary",LastName="Jane", Street="12 Any St", City="Manhattan", State="KS", Zip="50001", Age=33, Interests="Running, Reading"},
                new HCperson{FirstName="Joe",LastName="Montana", Street="1 Some St", City="Miami", State="FL", Zip="20001", Age=35, Interests="Billiards, Reading"},
                new HCperson{FirstName="Jennifer",LastName="Williams", Street="233 Ocean St", City="Fargo", State="ND", Zip="60001", Age=54, Interests="Hunting, Reading"},
                new HCperson{FirstName="Thomas",LastName="Jefferson", Street="23 Tower St", City="Ellicot City", State="MD", Zip="20001", Age=95, Interests="Roller Blading, Reading"},
                new HCperson{FirstName="George",LastName="Washington", Street="1 Penn Ave", City="Trenton", State="NJ", Zip="08543", Age=85, Interests="Hunting, Reading"},
                new HCperson{FirstName="Abraham",LastName="Lincoln", Street="1323 Hat St", City="New York", State="NY", Zip="10002", Age=78, Interests="Theater, Reading"},
                new HCperson{FirstName="John",LastName="Kennedy", Street="3 Parish Drive", City="Lebanon", State="PA", Zip="30201", Age=57, Interests="Surfing, Reading"},
                new HCperson{FirstName="Richard",LastName="Nixon", Street="3 Overlook Drive", City="Baltimore", State="MD", Zip="30001", Age=78, Interests="Recording, Reading"},
                new HCperson{FirstName="Ronald",LastName="Reagen", Street="9 Jackson St", City="New York", State="NY", Zip="10003", Age=89, Interests="Acting, Reading"},
                new HCperson{FirstName="William",LastName="Clinton", Street="10 Main St", City="New York", State="NY", Zip="10005", Age=56, Interests="Music, Reading"},
                new HCperson{FirstName="Albert",LastName="Brooks", Street="131 Canary St", City="Atlanta", State="GA", Zip="50001", Age=71, Interests="Writing, Reading"},
                new HCperson{FirstName="Robert",LastName="Meadows", Street="12343 Main St", City="New York", State="NY", Zip="10005",Age=84, Interests="Jogging, Reading"},
                new HCperson{FirstName="Martha",LastName="Ridge", Street="12333 West End St", City="New York", State="NY", Zip="10007", Age=56, Interests="Decorating, Reading"},
                new HCperson{FirstName="John",LastName="Smith", Street="1457 Madison Ave", City="New York", State="NY", Zip="10008", Age=41, Interests="Soccer, Reading"}
            };
            foreach (HCperson s in hcPersons)
            {
                context.HCpersons.Add(s);
            }
            context.SaveChanges();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
