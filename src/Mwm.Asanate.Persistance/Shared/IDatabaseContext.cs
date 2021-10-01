using Microsoft.EntityFrameworkCore;
using Mwm.Asanate.Domain;

namespace Mwm.Asanate.Persistance.Shared {

    public interface IDatabaseContext {
        DbSet<User> Users { get; set; }

        DbSet<Project> Projects { get; set; }

        DbSet<Company> Companies { get; set; }

        DbSet<Initiative> Initiatives { get; set; }

        DbSet<Tsk> Tsks { get; set; }

        //DbSet<Workspace> Workspaces { get; set; }

        DbSet<T> Set<T>() where T : class, IEntity;

        void Save();

        void RecreateDatabase();

        void EnsureCreated();

        void EnsureDeleted();
    }
}

