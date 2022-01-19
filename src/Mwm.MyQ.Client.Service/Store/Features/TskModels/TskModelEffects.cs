using Fluxor;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Helpers;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Effects;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Store.Features.TskModels;

public class TskTriggeredTskLoadModelEffect : TskLoadModelEffect<Tsk> {
    public TskTriggeredTskLoadModelEffect(ILogger<TskLoadModelEffect<Tsk>> logger, 
                                          IState<EntityState<Tsk>> tsksState, 
                                          IState<EntityState<Initiative>> initiativesState, 
                                          IState<EntityState<Project>> projectsState, 
                                          IState<EntityState<Company>> companiesState) : 
        base(logger, tsksState, initiativesState, projectsState, companiesState) {
    }
}

public class InitiativeTriggeredTskLoadModelEffect : TskLoadModelEffect<Initiative> {
    public InitiativeTriggeredTskLoadModelEffect(ILogger<TskLoadModelEffect<Initiative>> logger,
                                          IState<EntityState<Tsk>> tsksState,
                                          IState<EntityState<Initiative>> initiativesState,
                                          IState<EntityState<Project>> projectsState,
                                          IState<EntityState<Company>> companiesState) :
        base(logger, tsksState, initiativesState, projectsState, companiesState) {
    }
}

public class ProjectTriggeredTskLoadModelEffect : TskLoadModelEffect<Project> {
    public ProjectTriggeredTskLoadModelEffect(ILogger<TskLoadModelEffect<Project>> logger,
                                          IState<EntityState<Tsk>> tsksState,
                                          IState<EntityState<Initiative>> initiativesState,
                                          IState<EntityState<Project>> projectsState,
                                          IState<EntityState<Company>> companiesState) :
        base(logger, tsksState, initiativesState, projectsState, companiesState) {
    }
}

public class CompanyTriggeredTskLoadModelEffect : TskLoadModelEffect<Company> {
    public CompanyTriggeredTskLoadModelEffect(ILogger<TskLoadModelEffect<Company>> logger,
                                          IState<EntityState<Tsk>> tsksState,
                                          IState<EntityState<Initiative>> initiativesState,
                                          IState<EntityState<Project>> projectsState,
                                          IState<EntityState<Company>> companiesState) :
        base(logger, tsksState, initiativesState, projectsState, companiesState) {
    }
}

public abstract class TskLoadModelEffect<TEntity> : LoadModelEffect<TEntity, Tsk> where TEntity : INamedEntity {

    private IState<EntityState<Initiative>> _initiativesState { get; set; }

    private IState<EntityState<Project>> _projectsState { get; set; }

    private IState<EntityState<Company>> _companiesState { get; set; }

    public TskLoadModelEffect(ILogger<TskLoadModelEffect<TEntity>> logger,
                              IState<EntityState<Tsk>> tsksState,
                              IState<EntityState<Initiative>> initiativesState,
                              IState<EntityState<Project>> projectsState,
                              IState<EntityState<Company>> companiesState) : base(logger, tsksState) {
        _initiativesState = initiativesState;
        _projectsState = projectsState;
        _companiesState = companiesState;
    }

    public bool HasValues() => _state.HasValue(true) &&
                           _initiativesState.HasValue(true) &&
                           _projectsState.HasValue(true) &&
                           _companiesState.HasValue(true);

    public override Task HandleAsync(LoadModelAction<TEntity> action, IDispatcher dispatcher) {
        if (HasValues())
            return base.HandleAsync(action, dispatcher);
        return Task.CompletedTask;
    }

    public override EntityModel<Tsk> CreateModel(Tsk t) {
        Initiative initiative = _initiativesState.FindById(t.InitiativeId);
        Project project = (initiative != null) ? _projectsState.FindById(initiative.ProjectId) : null;
        Company company = (project != null) ? _companiesState.FindById(project.CompanyId) : null;

        return new TskModel {
            Id = t.Id,
            Name = t.Name,
            Status = t.Status,
            DurationEstimate = t.DurationEstimate,
            DurationCompleted = t.DurationCompleted,
            Notes = t.Notes,
            CreatedDate = t.CreatedDate,
            DueDate = t.DueDate,
            StartDate = t.StartDate,
            StartedDate = t.StartedDate,
            CompletedDate = t.CompletedDate,
            InitiativeName = initiative?.Name,
            InitiativeExternalId = EntityHelpers.ToExternalId(initiative, project),
            InitiativeExternalLink = EntityHelpers.ToExternalLink(initiative, project),
            ProjectName = project?.Name,
            ProjectAbbreviation = project?.Abbreviation,
            CompanyName = company?.Name,
            ProjectColor = project?.Color,
            IsInFocus = t.IsInFocus
        };
    }

}
