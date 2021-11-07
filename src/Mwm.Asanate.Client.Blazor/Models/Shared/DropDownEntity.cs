using Mwm.Asanate.Domain;

namespace Mwm.Asanate.Client.Blazor.Models.Shared {
    public class DropDownEntity {

        public int Id {  get; set; }
        public string Name { get; set; }

        public int Index { get; set; } = 0;

    }

    public static class DropDownEntityExtensions {

        public static DropDownEntity ToDropDownEntity(this Project project, int index) {
            if (project.Name == Project.DefaultProjectName) 
                return new DropDownEntity { Id = project.Id, Name = $"- {project.Name} -" };
            return new DropDownEntity { Id = project.Id, Name = project.Name, Index = index };
        }

        public static DropDownEntity ToDropDownEntity(this Initiative initiative, int index) {
            if (initiative.Name == Initiative.DefaultInitiativeName)
                return new DropDownEntity { Id = initiative.Id, Name = $"- {initiative.Name} -" };
            return new DropDownEntity { Id = initiative.Id, Name = initiative.Name, Index = index };

        }

    }

}
