using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mwm.Asanate.Data.Utils {
    public static class DatabaseContextExtensions {

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("DatabaseContext");
            services.AddDatabaseContext(connectionString);
            return services;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string connectionString) {
            services.AddDbContext<IDatabaseContext, DatabaseContext>(
                opt => {
                    if (connectionString == "InMemory") {
                        opt.UseInMemoryDatabase("Asanate.db");
                        Console.WriteLine("Using InMemoryDatabase.");
                    } else if (connectionString == "SQLite") {
                        opt.UseSqlite(@"Data Source=Asanate.db;Cache=Shared");
                        Console.WriteLine("Using Sqlite.");
                    } else {
                        Console.WriteLine($"Using Database: {connectionString}.");
                        opt.UseSqlServer(connectionString);
                    }
                    //opt.EnableSensitiveDataLogging();
                }
            );
            return services;
        }


        //public static DatabaseContext RecreateDatabase(this DatabaseContext databaseContext) {
        //    databaseContext.Database.EnsureDeleted();
        //    databaseContext.Database.EnsureCreated();
        //    return databaseContext;
        //}

    }
}
