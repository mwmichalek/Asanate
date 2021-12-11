using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mwm.MyQ.Data.Utils {
    public static class DatabaseContextExtensions {

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("DatabaseContext");
            services.AddDatabaseContext(connectionString);
            return services;
        }

        private static bool LoggedDatabaseMessage;

        private static void LogDatabaseMessage(string msg) {
            if (!LoggedDatabaseMessage) {
                LoggedDatabaseMessage = true;
                Console.WriteLine(msg);
            }
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string connectionString) {
            services.AddDbContext<IDatabaseContext, DatabaseContext>(
                opt => {
                    if (connectionString == "InMemory") {
                        opt.UseInMemoryDatabase("MyQ.db");
                        LogDatabaseMessage("Using InMemoryDatabase.");
                    } else if (connectionString == "SQLite") {
                        opt.UseSqlite(@"Data Source=MyQ.db;Cache=Shared");
                        LogDatabaseMessage("Using Sqlite.");
                    } else {
                        LogDatabaseMessage($"Using Database: {connectionString}.");
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
