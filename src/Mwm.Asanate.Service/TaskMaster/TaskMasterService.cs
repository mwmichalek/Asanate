﻿using Mwm.Asanate.Model;
using Mwm.Asanate.Service.AsanaApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //foreach (var project in (await asanaService.GetAll<Project>()).OrderBy(p => p.Company).ThenBy(p => p.Name)) {
            //    Console.WriteLine($"Company: {project.Company}, Project: {project.Name}");
            //}

            var sw = new Stopwatch();
            sw.Start();

            var tskResult = await asanaService.RetrieveAll<Tsk>();

            if (tskResult.IsSuccess) {
                var atasks = tskResult.Value
                                    .Where(t => !t.IsCompleted)
                                    .OrderByDescending(t => t.CreatedAt)
                                    .ToList();


                foreach (var atask in atasks) {
                    Console.WriteLine(atask);
                }

                Console.WriteLine(sw.ElapsedMilliseconds);

            }

            
        }
    }
}
