using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Mwm.MyQ.Client.Blayzor.Helpers;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Blayzor.Components;

public abstract class TskModelConsumerComponent : EventHandlerComponent {

    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();
        await BuildTskModels();
    }

    protected List<TskModel> tskModels = new List<TskModel>();

    public IEnumerable<TskModel> TskModels {
        get => tskModels;
        set { }
    }

    protected override async Task HandleUpdateAsync(Company _) => await BuildTskModels();
    protected override async Task HandleUpdateAsync(Project _) => await BuildTskModels();
    protected override async Task HandleUpdateAsync(Initiative _) => await BuildTskModels();
    protected override async Task HandleUpdateAsync(Tsk _) => await BuildTskModels();

    protected virtual async Task BuildTskModels() {
        if (HasValues()) {
            var tsks = TsksState.Value.Entities.Where(t => !t.IsArchived);
            tskModels = tsks.Select(t => CreateModel(t)).ToList();
            await HandleUpdate(tskModels);
        }
    }

    protected virtual Task HandleUpdate(IEnumerable<TskModel> tskModels) => Task.CompletedTask;

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
