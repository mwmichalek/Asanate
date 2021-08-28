using Mwm.Asanate.Model;
using Mwm.Asanate.Service.AsanaApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Service.TaskMaster {

    public interface ITaskMasterService {

        Task Test();

    }

    public class TaskMasterService : ITaskMasterService {

        private IAsanaService asanaService;

        public TaskMasterService(IAsanaService asanaService) {
            this.asanaService = asanaService;
        }

        public async Task Test() {
            foreach (var project in await asanaService.GetAll<Project>()) {
                Console.WriteLine($"Project: {project.Name}");
            }
        }
    }
}
