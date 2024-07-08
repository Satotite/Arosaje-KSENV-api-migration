using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Arosaje_KSENV.Models;

namespace DatabaseMergeVerification
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ArosajeKsenvContext>();

                var utilisateurs = context.Utilisateurs.ToList();
                Console.WriteLine($"Nombre d'utilisateurs: {utilisateurs.Count}");

                var villes = context.Villes.ToList();
                Console.WriteLine($"Nombre de villes: {villes.Count}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<ArosajeKsenvContext>(options =>
                        options.UseNpgsql("Host=localhost;Database=Arosaje_Database_MySql;Username=postgres;Password=epsi"));
                });
    }
}
