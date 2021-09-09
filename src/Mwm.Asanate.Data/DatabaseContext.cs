using Microsoft.EntityFrameworkCore;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Data {
    public interface IDatabaseContext {
        DbSet<User> Users { get; set; }

        DbSet<Project> Projects { get; set; }

        DbSet<Company> Companies { get; set; }

        DbSet<Initiative> Initiatives { get; set; }

        DbSet<Status> Statuses { get; set; }

        DbSet<Tsk> Tsks { get; set; }

        //DbSet<Workspace> Workspaces { get; set; }

        DbSet<T> Set<T>() where T : class, IAsanateEntity;

        void Save();
    }
    public class DatabaseContext : DbContext, IDatabaseContext {
        private readonly bool IsExternallyConfigured = false;

        public DatabaseContext() : base() {
            Console.WriteLine("Migration In The Hayouse!");
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            IsExternallyConfigured = true;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Initiative> Initiatives { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Tsk> Tsks { get; set; }

        public void Save() {
            this.SaveChanges();
        }

        public new DbSet<T> Set<T>() where T : class, IAsanateEntity {
            return base.Set<T>();
        }
    }
}




//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Olive.LRP.Domain;
//using Olive.LRP.Domain.Common;
//using Olive.LRP.Persistance.Shared;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace Olive.LRP.Data
//{
//    public class DatabaseContext : DbContext, IDatabaseContext
//    {

//        private readonly bool IsExternallyConfigured = false;

//        public DatabaseContext() : base()
//        {
//            Console.WriteLine("Migration In The Hayouse!");

//        }

//        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
//        {
//            IsExternallyConfigured = true;
//        }

//        public DbSet<Member> Members { get; set; }

//        public DbSet<CreditGrant> CreditGrants { get; set; }
//        public DbSet<CreditTransaction> CreditTransactions { get; set; }

//        public void Save()
//        {
//            this.SaveChanges();
//        }

//        public new DbSet<T> Set<T>() where T : class, IEntity
//        {
//            return base.Set<T>();
//        }

//        // NOTE: This is only being used by Entity Framework Migration Tools.  
//        // For some reason the setup within Console/WebApi isn't working, will 
//        // probably have more luck with the FunctionApp
//        protected override void OnConfiguring(DbContextOptionsBuilder opt)
//        {
//            if (!IsExternallyConfigured)
//            {
//                //opt.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Olive.LRP;Trusted_Connection=True;");
//                opt.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Olive.LRP");
//                opt.EnableSensitiveDataLogging();
//                base.OnConfiguring(opt);
//            }
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            modelBuilder.ApplyConfiguration(new MemberConfiguration());
//            modelBuilder.ApplyConfiguration(new CreditGrantConfiguration());
//            modelBuilder.ApplyConfiguration(new CreditTransactionConfiguration());
//        }
//    }

//    public class MemberConfiguration : IEntityTypeConfiguration<Member>
//    {
//        public void Configure(EntityTypeBuilder<Member> builder)
//        {

//            builder.HasKey(m => m.Id);
//            builder.Property(m => m.Id).ValueGeneratedNever();

//        }
//    }

//    public class CreditGrantConfiguration : IEntityTypeConfiguration<CreditGrant>
//    {
//        public void Configure(EntityTypeBuilder<CreditGrant> builder)
//        {

//            builder.HasIndex(cg => cg.Id);

//            // To prevent duplicates we add additional columns to the index
//            //builder.HasIndex(cg => cg.MemberId)
//            //       .IncludeProperties(cg => new { cg.GrantType, cg.ExpirationDate, cg.CreditBalance });
//            builder.HasIndex("MemberId", "GrantType", "ExpirationDate", "CreditBalance")
//                   .IsUnique();

//        }
//    }

//    public class CreditTransactionConfiguration : IEntityTypeConfiguration<CreditTransaction>
//    {
//        public void Configure(EntityTypeBuilder<CreditTransaction> builder)
//        {

//        }
//    }
//}
