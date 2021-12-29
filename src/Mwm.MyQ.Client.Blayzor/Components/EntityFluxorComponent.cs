using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Mwm.MyQ.Client.Blayzor.Helpers;
using Mwm.MyQ.Client.Blayzor.Models.Tsks;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mwm.MyQ.Client.Blayzor.Components;

public abstract class EntityFluxorComponent : FluxorComponent {

    [Inject]
    public IState<EntityState<Tsk>> TsksState { get; set; }

    [Inject]
    public IState<EntityState<Initiative>> InitiativesState { get; set; }

    [Inject]
    public IState<EntityState<Project>> ProjectsState { get; set; }

    [Inject]
    public IState<EntityState<Company>> CompaniesState { get; set; }

    [Inject]
    public EntityStateFacade EntityStateFacade { get; set; }

    public bool IsLoading() => TsksState.IsLoading() ||
                           InitiativesState.IsLoading() ||
                           ProjectsState.IsLoading() ||
                           CompaniesState.IsLoading();

    public bool HasErrors() => TsksState.HasErrors() ||
                               InitiativesState.HasErrors() ||
                               ProjectsState.HasErrors() ||
                               CompaniesState.HasErrors();

    public bool HasValues() => TsksState.HasValue(true) &&
                               InitiativesState.HasValue(true) &&
                               ProjectsState.HasValue(true) &&
                               CompaniesState.HasValue(true);

    protected override void OnInitialized() {
        if (!TsksState.HasValue())
            EntityStateFacade.Load<Tsk>();
        if (!InitiativesState.HasValue())
            EntityStateFacade.Load<Initiative>();
        if (!CompaniesState.HasValue())
            EntityStateFacade.Load<Company>();
        if (!ProjectsState.HasValue())
            EntityStateFacade.Load<Project>();

        TsksState.StateChanged += (s, e) => BuildTskModels();
        InitiativesState.StateChanged += (s, e) => BuildTskModels();
        ProjectsState.StateChanged += (s, e) => BuildTskModels();
        CompaniesState.StateChanged += (s, e) => BuildTskModels();

        BuildTskModels();

        base.OnInitialized();
    }

    protected List<TskModel> tskModels = new List<TskModel>();

    public IEnumerable<TskModel> TskModels {
        get => tskModels;
        set { }
    }

    protected virtual void BuildTskModels() {
        if (HasValues()) {
            var tsks = TsksState.Value.Entities.Where(t => !t.IsArchived);
            tskModels = tsks.Select(t => CreateModel(t)).ToList();
        }
    }

    protected virtual TskModel CreateModel(Tsk t) {
        Initiative initiative = InitiativesState.FindById(t.InitiativeId);
        Project project = (initiative != null) ? ProjectsState.FindById(initiative.ProjectId) : null;
        Company company = (project != null) ? CompaniesState.FindById(project.CompanyId) : null;

        return new TskModel {
            Id = t.Id,
            Name = t.Name,
            Status = t.Status,
            DurationEstimate = t.DurationEstimate,
            DurationCompleted = t.DurationCompleted,
            Notes = t.Notes,
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
