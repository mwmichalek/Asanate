using Fluxor;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;

namespace Mwm.MyQ.Client.Mud.Models.Shared {
    public class DropDown<TEntity> where TEntity : INamedEntity {

        public DropDown(TEntity entity) => Entity = entity;

        public TEntity Entity { get; set; }

        public override string ToString() {
            return Entity.Name;
        }

    }

    //public static class DropDownEntityExtensions {

    //    public static DropDownEntity ToDropDownEntity(this Project project, IState<EntityState<Company>> CompaniesState, int index) {
    //        Company company = (project != null) ? CompaniesState.FindById(project.CompanyId) : null;
    //        if (project.Name == Project.DefaultProjectName) 
    //            return new DropDownEntity { Id = project.Id, Name = $"- {project.Name} -" };
    //        return new DropDownEntity { Id = project.Id, Name = $"{company.Name} - {project.Name}", Index = index };
    //    }

    //    public static DropDownEntity ToDropDownEntity(this Initiative initiative, int index) {
    //        if (initiative.Name == Initiative.DefaultInitiativeName)
    //            return new DropDownEntity { Id = initiative.Id, Name = $"- {initiative.Name} -" };
    //        return new DropDownEntity { Id = initiative.Id, Name = initiative.Name, Index = index };

    //    }

    //}

}
