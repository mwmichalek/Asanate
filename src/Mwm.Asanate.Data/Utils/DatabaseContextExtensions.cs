using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mwm.Asanate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mwm.Asanate.Data.Utils {
    public static class DatabaseContextExtensions {

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<DatabaseContext>(
                opt => {
                    if (configuration.GetConnectionString("DatabaseContext") == "InMemory") {
                        opt.UseInMemoryDatabase("Asanate.db");
                    } else if (configuration.GetConnectionString("DatabaseContext") == "SQLite") {
                        opt.UseSqlite(@"Data Source=Asanate.db;Cache=Shared");
                    } else {
                        var connectionString = configuration.GetConnectionString("DatabaseContext");
                        opt.UseSqlServer(connectionString);
                    }
                    //opt.EnableSensitiveDataLogging();
                }
            );

            services.AddScoped<IDatabaseContext>((serviceProvider) => serviceProvider.GetService<DatabaseContext>());
            return services;
        }


        public static DatabaseContext RecreateDatabase(this DatabaseContext databaseContext) {
            databaseContext.Database.EnsureDeleted();
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

    }
}
