using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mwm.MyQ.Domain;
using Mwm.MyQ.Persistance.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Data {
    
    public class DatabaseContext : DbContext, IDatabaseContext {
        //private readonly bool IsExternallyConfigured = false;

        public DatabaseContext() : base() {
            Console.WriteLine("Migration In The Hayouse!");
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            //IsExternallyConfigured = true;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Initiative> Initiatives { get; set; }

        public DbSet<Tsk> Tsks { get; set; }

        public void Save() {
            this.SaveChanges();
        }

        public Task SaveAsync() {
            return this.SaveChangesAsync();
        }

        public new DbSet<T> Set<T>() where T : class, IEntity {
            return base.Set<T>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder opt) {
            //if (!IsExternallyConfigured) {
                //opt.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Olive.LRP;Trusted_Connection=True;");
                //opt.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Olive.LRP");
                //opt.EnableSensitiveDataLogging();
                base.OnConfiguring(opt);
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TskConfiguration());
        }

        public void EnsureCreated() {
            Database.EnsureCreated();
        }

        public void EnsureDeleted() {
            Database.EnsureDeleted();
        }
        public void RecreateDatabase() {
            EnsureDeleted();
            EnsureCreated();
        }
    }

    public class TskConfiguration : IEntityTypeConfiguration<Tsk> {
        public void Configure(EntityTypeBuilder<Tsk> builder) {
            //builder.Property<User>("C".WithMany().IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
