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
            private readonly IRepository<User> _userRepository;
            private readonly IRepository<Tsk> _tskRepository;
            private readonly IRepository<Initiative> _initiativeRepository;

            private readonly IAsanaService<AsanaProject> _asanaProjectService;
            private readonly IAsanaService<AsanaUser> _asanaUserService;
            private readonly IAsanaService<AsanaTsk> _asanaTskService;

            public Handler(IRepository<Project> projectRepository,
                           IRepository<Company> companyRepository,
                           IRepository<User> userRepository,
                           IRepository<Tsk> tskRepository,
                           IRepository<Initiative> initiativeRepository,
                           IAsanaService<AsanaProject> asanaProjectService,
                           IAsanaService<AsanaUser> asanaUserService,
                           IAsanaService<AsanaTsk> asanaTskService) {
                _projectRepository = projectRepository;
                _companyRepository = companyRepository;
                _userRepository = userRepository;
                _tskRepository = tskRepository;
                _initiativeRepository = initiativeRepository;

                _asanaProjectService = asanaProjectService;
                _asanaUserService = asanaUserService;
                _asanaTskService = asanaTskService;
            }

            protected override Result Handle(Command command) {

                var userResult = SyncUsers();
                if (userResult.IsFailed)
                    return userResult;

                var taskAndInitiativesResult = SyncTaskAndInitiatives();
                if (taskAndInitiativesResult.IsFailed)
                    return taskAndInitiativesResult;

                var projectsAndCompaniesResult = SyncProjectsAndCompanies();
                if (projectsAndCompaniesResult.IsFailed) 
                    return projectsAndCompaniesResult;

                return Result.Ok();
            }

            private Result SyncTaskAndInitiatives() {
                var tskResults = _asanaTskService.RetrieveAll().Result;

                if (tskResults.TryUsing(out List<AsanaTsk> asanaTsks)) {
                    var requiresSaving = false;


                    foreach (var asanaTsk in asanaTsks.Select(at => at.SubProjectName).Distinct()) { 
                        //TODO: Make initiatives.
                    }

                    foreach (var asanaTsk in asanaTsks) {
                        
                        _tskRepository.Add(new Tsk
                        {
                            //TODO: Map properties
                            Name = asanaTsk.Name
                        });
                    }

                    if (requiresSaving)
                        _tskRepository.Save();

                    return Result.Ok();
                }

                return Result.Fail("Unable to retreive Asana User");
            }

            private Result SyncUsers() {
                var userResults = _asanaUserService.RetrieveAll().Result;

                if (userResults.TryUsing(out List<AsanaUser> asanaUsers)) {
                    var requiresSaving = false;

                    foreach (var asanaUser in asanaUsers) {
                        var firstAndLast = asanaUser.Name.Split(" ");
                        _userRepository.Add(new User
                        {
                            Name = asanaUser.Name,
                            FirstName = firstAndLast[0],
                            LastName = firstAndLast[1]
                        });
                    }

                    if (requiresSaving)
                        _projectRepository.Save();

                    return Result.Ok();
                }

                return Result.Fail("Unable to retreive Asana User");
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






