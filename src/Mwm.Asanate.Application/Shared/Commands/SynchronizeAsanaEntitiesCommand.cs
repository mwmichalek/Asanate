using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Mwm.Asana.Model;
using Mwm.Asana.Model.Converters;
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

            private ILogger<Handler> _logger;

            private readonly IRepository<Project> _projectRepository;
            private readonly IRepository<Company> _companyRepository;
            private readonly IRepository<User> _userRepository;
            private readonly IRepository<Tsk> _tskRepository;
            private readonly IRepository<Initiative> _initiativeRepository;

            private readonly IAsanaService<AsanaProject> _asanaProjectService;
            private readonly IAsanaService<AsanaUser> _asanaUserService;
            private readonly IAsanaService<AsanaTsk> _asanaTskService;

            public Handler(ILogger<Handler> logger,
                           IRepository<Project> projectRepository,
                           IRepository<Company> companyRepository,
                           IRepository<User> userRepository,
                           IRepository<Tsk> tskRepository,
                           IRepository<Initiative> initiativeRepository,
                           IAsanaService<AsanaProject> asanaProjectService,
                           IAsanaService<AsanaUser> asanaUserService,
                           IAsanaService<AsanaTsk> asanaTskService) {
                _logger = logger;

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

                var projectsAndCompaniesResult = SyncProjectsAndCompanies();
                if (projectsAndCompaniesResult.IsFailed)
                    return projectsAndCompaniesResult;

                var taskAndInitiativesResult = SyncTaskAndInitiatives();
                if (taskAndInitiativesResult.IsFailed)
                    return taskAndInitiativesResult;

                return Result.Ok();
            }

            private Result SyncUsers() {
                var userResults = _asanaUserService.RetrieveAll().Result;

                if (userResults.TryUsing(out List<AsanaUser> asanaUsers)) {
                    var requiresSaving = false;

                    foreach (var asanaUser in asanaUsers) {
                        var existingUser = _userRepository.GetByGid(asanaUser.Gid);
                        var firstAndLast = asanaUser.Name.Split(" ");
                        if (existingUser == null) {
                            _userRepository.Add(new User {
                                Name = asanaUser.Name,
                                FirstName = firstAndLast[0],
                                LastName = firstAndLast[1]
                            });
                            requiresSaving = true;
                        } else {
                            existingUser.Name = asanaUser.Name;
                            existingUser.FirstName = firstAndLast[0];
                            existingUser.LastName = firstAndLast[1];
                            requiresSaving = true;
                        }
                    }

                    if (requiresSaving)
                        _userRepository.Save();

                    return Result.Ok();
                }

                return Result.Fail("Unable to retreive Asana User");
            }

            private Result SyncProjectsAndCompanies() {
                var projectResults = _asanaProjectService.RetrieveAll().Result;

                if (projectResults.TryUsing(out List<AsanaProject> asanaProjects)) {
                    var requiresSaving = false;

                    foreach (var asanaCompanyName in asanaProjects.Select(ap => ap.Company).Distinct()) {
                        var existingCompany = _companyRepository.GetByName(asanaCompanyName);
                        if (existingCompany == null) {
                            _companyRepository.Add(new Company {
                                Name = asanaCompanyName
                            });
                            requiresSaving = true;
                        }
                    }

                    if (requiresSaving)
                        _companyRepository.Save();
                    requiresSaving = false;

                    foreach (var asanaProject in asanaProjects) {
                        var existingProject = _projectRepository.GetByGid(asanaProject.Gid);
                        if (existingProject == null) {
                            var newProject = new Project {
                                Name = asanaProject.Name,
                                Gid = asanaProject.Gid,
                                Company = _companyRepository.GetByName(asanaProject.Company),
                            };
                            newProject.Initiatives.Add(new Initiative {
                                Name = "Triage"
                            });
                            _projectRepository.Add(newProject);
                            requiresSaving = true;
                        } else {
                            existingProject.Name = asanaProject.Name;
                            existingProject.Color = asanaProject.Color;
                            requiresSaving = true;
                        }
                    }

                    if (requiresSaving)
                        _projectRepository.Save();

                    return Result.Ok();
                }

                return Result.Fail("Unable to retreive Asana Projects");
            }

            private Result SyncTaskAndInitiatives() {
                var tskResults = _asanaTskService.RetrieveAll().Result;

                if (tskResults.TryUsing(out List<AsanaTsk> asanaTsks)) {
                    var requiresSaving = false;

                    foreach (var asanaTsk in asanaTsks.Where(t => !string.IsNullOrEmpty(t.ProjectName))) {
                        var initiativeName = string.IsNullOrEmpty(asanaTsk.SubProjectName) ? "Triage" : asanaTsk.SubProjectName;
                        var existingInitiative = _initiativeRepository.GetAll().SingleOrDefault(i => i.Name == initiativeName &&
                                                                                                i.Project.Name == asanaTsk.ProjectName);
                        if (existingInitiative == null) {
                            var existingProject = _projectRepository.GetByName(asanaTsk.ProjectName);

                            _logger.LogDebug($"New Initiative: {existingProject?.Name}");
                            existingInitiative = new Initiative {
                                Name = asanaTsk.SubProjectName,
                                Project = existingProject
                            };
                            _initiativeRepository.Add(existingInitiative);
                            requiresSaving = true;
                        }
                        if (requiresSaving)
                            _tskRepository.Save();
                        requiresSaving = false;

                        var existingTsk = _tskRepository.GetByGid(asanaTsk.Gid);
                        if (existingTsk == null) {
                            _tskRepository.Add(new Tsk {
                                Name = asanaTsk.Name,
                                Status = asanaTsk.Status.ToStatus(),
                                Notes = asanaTsk.Notes,
                                CompletedDate = asanaTsk.CompletedAt.ToDateTime(),
                                IsCompleted = asanaTsk.IsCompleted,
                                CreatedDate = asanaTsk.CreatedAt.ToDateTime(),
                                DueDate = asanaTsk.CreatedAt.ToDateTime(),
                                StartedDate = asanaTsk.StartedOn.ToDateTime(),
                                Initiative = existingInitiative,
                                AssignedTo = _userRepository.GetByName(asanaTsk.AssignedTo.Name)
                            });
                            requiresSaving = true;
                        } else {
                            existingTsk.Name = asanaTsk.Name;
                            existingTsk.Status = asanaTsk.Status.ToStatus();
                            existingTsk.Notes = asanaTsk.Notes;
                            existingTsk.CompletedDate = asanaTsk.CompletedAt.ToDateTime();
                            existingTsk.IsCompleted = asanaTsk.IsCompleted;
                            existingTsk.CreatedDate = asanaTsk.CreatedAt.ToDateTime();
                            existingTsk.DueDate = asanaTsk.CreatedAt.ToDateTime();
                            existingTsk.StartedDate = asanaTsk.StartedOn.ToDateTime();
                            existingTsk.Initiative = existingInitiative;
                            existingTsk.AssignedTo = _userRepository.GetByName(asanaTsk.AssignedTo.Name);
                            requiresSaving = true;
                        }

                        if (requiresSaving)
                            _tskRepository.Save();
                    }

                    

                    return Result.Ok();
                }

                return Result.Fail("Unable to retreive Asana User");
            }
        }
    }
}






