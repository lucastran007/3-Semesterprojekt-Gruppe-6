using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Blazor.Shared;

namespace Blazor.Server.Data.Seeders
{
    public class SurfBoardSeeder
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            IOptions<OperationalStoreOptions> operationalStoreOptions = serviceProvider.GetRequiredService<IOptions<OperationalStoreOptions>>();
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>(), operationalStoreOptions))
            {
                // Look for any surfboards.
                if (context.SurfBoard.Any())
                {
                    Console.WriteLine("[~] Surfboards have already been Seeded, continuing ...");
                    return;   // DB has been seeded
                }

                context.SurfBoard.AddRange(
                    new SurfBoard
                    {
                        BoardName = "The Minilog",
                        Thickness = 2.75F,
                        Width = 21,
                        Length = 6,
                        Volume = 38.8F,
                        Boardtype = "Shortboard",
                        Price = 565,
                        Equipment = "",
                        ImageName = ""
                    },
                    new SurfBoard
                    {
                        BoardName = "The Wide Glider",
                        Thickness = 2.75F,
                        Width = 21.75F,
                        Length = 7.1F,
                        Volume = 44.16F,
                        Boardtype = "Funboard",
                        Price = 685,
                        Equipment = "",
                        ImageName = ""
                    },
                    new SurfBoard
                    {
                        BoardName = "The Golden Ratio",
                        Thickness = 2.9F,
                        Width = 21.85F,
                        Length = 6.3F,
                        Volume = 43.22F,
                        Boardtype = "Funboard",
                        Price = 695,
                        Equipment = "",
                        ImageName = ""
                    },
                    new SurfBoard
                    {
                        BoardName = "Mahi Mahi",
                        Thickness = 2.3F,
                        Width = 20.75F,
                        Length = 5.4F,
                        Volume = 29.39F,
                        Boardtype = "Fish",
                        Price = 645,
                        Equipment = "",
                        ImageName = ""
                    },
                    new SurfBoard
                    {
                        BoardName = "The Emerald Glider",
                        Thickness = 2.8F,
                        Width = 22.8F,
                        Length = 9.2F,
                        Volume = 65.4F,
                        Boardtype = "Longboard",
                        Price = 895,
                        Equipment = "",
                        ImageName = ""
                    },
                    new SurfBoard
                    {
                        BoardName = "The Bomb",
                        Thickness = 2.5F,
                        Width = 21,
                        Length = 5.5F,
                        Volume = 33.7F,
                        Boardtype = "Shortboard",
                        Price = 645,
                        Equipment = "",
                        ImageName = ""
                    },
                    new SurfBoard
                    {
                        BoardName = "Walden Magic",
                        Thickness = 3F,
                        Width = 19.4F,
                        Length = 9.6F,
                        Volume = 80F,
                        Boardtype = "Longboard",
                        Price = 1025,
                        Equipment = "",
                        ImageName = ""
                    },
                    new SurfBoard
                    {
                        BoardName = "Naish One",
                        Thickness = 6F,
                        Width = 30,
                        Length = 12.6F,
                        Volume = 301F,
                        Boardtype = "SUP",
                        Price = 854,
                        Equipment = "Paddle",
                        ImageName = ""
                    },
                    new SurfBoard
                    {
                        BoardName = "STX Tourer",
                        Thickness = 6F,
                        Width = 32,
                        Length = 11.6F,
                        Volume = 270F,
                        Boardtype = "SUP",
                        Price = 611,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImageName = ""
                    },
                    new SurfBoard
                    {
                        BoardName = "Naish Maliko",
                        Thickness = 6F,
                        Width = 25,
                        Length = 14,
                        Volume = 330F,
                        Boardtype = "SUP",
                        Price = 1304,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImageName=""
                    }
                );
                context.SaveChanges();
                Console.WriteLine("[+] Surfboards have been Seeded!");
            }

        }
    }
}
