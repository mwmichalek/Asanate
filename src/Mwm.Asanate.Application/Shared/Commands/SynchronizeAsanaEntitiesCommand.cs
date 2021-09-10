using FluentResults;
using MediatR;
using Mwm.Asana.Model;
using Mwm.Asana.Service;
using Mwm.Asanate.Application.Interfaces.Persistance;
using Mwm.Asanate.Application.Utils;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mwm.Asanate.Application.Shared.Commands {
    public class SynchronizeAsanaEntitiesCommand {

        public class Command : IRequest<Result> {
            public DateTime? Since { get; set; }

        }

        public class Handler : RequestHandler<Command, Result> {

            private readonly IRepository<Project> _projectRepository;
            private readonly IRepository<Company> _companyRepository;

            private readonly IAsanaService<AsanaProject> _asanaProjectService;

            public Handler(IRepository<Project> projectRepository,
                           IRepository<Company> companyRepository,
                           IAsanaService<AsanaProject> asanaProjectService) {
                _projectRepository = projectRepository;
                _companyRepository = companyRepository;
                _asanaProjectService = asanaProjectService;
            }

            protected override Result Handle(Command command) {

                var projectsAndCompaniesResult = SyncProjectsAndCompanies();
                if (projectsAndCompaniesResult.IsFailed) 
                    return projectsAndCompaniesResult;

                return Result.Ok();
            }

            private Result SyncProjectsAndCompanies() {
                var projectResults = _asanaProjectService.RetrieveAll().Result;

                if (projectResults.TryUsing(out List<AsanaProject> asanaProjects)) {
                    var requiresSaving = false;

                    foreach (var asanaCompanyName in asanaProjects.Select(ap => ap.Company).Distinct()) {
                        //TODO: (Check to see if they exist first)
                        _companyRepository.Add(new Company {
                            Name = asanaCompanyName
                        });
                        requiresSaving = true;
                    }

                    if (requiresSaving) 
                        _projectRepository.Save();
                    requiresSaving = false;

                    foreach (var asanaProject in asanaProjects) {
                        //TODO: (Check to see if they exist first)
                        _projectRepository.Add(new Project {
                            Name = asanaProject.Name,
                            Gid = asanaProject.Gid,
                            Company = _companyRepository.GetAll().SingleOrDefault(c => c.Name == asanaProject.Company)
                        });
                        requiresSaving = true;
                    }

                    if (requiresSaving) 
                        _projectRepository.Save();

                    return Result.Ok();
                }

                return Result.Fail("Unable to retreive Asana Projects");
            }
        }
    }
}






