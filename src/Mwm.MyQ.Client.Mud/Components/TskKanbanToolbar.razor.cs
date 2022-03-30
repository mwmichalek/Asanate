using Fluxor;
using Microsoft.AspNetCore.Components;
using Mwm.MyQ.Client.Service.Components;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.ModelFilters;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Mwm.MyQ.Client.Mud.Components;

public partial class TskKanbanToolbar : ModelConsumerComponent<TskModel, Tsk>,
                                        IApplicationSettingConsumer<IsInFocusOnlyFlag>,
                                        IApplicationSettingConsumer<IsGroupedByCompanyFlag>,
                                        IApplicationSettingConsumer<IsActionStatusOnlyFlag>,
                                        IApplicationSettingConsumer<IsTskQuickPaneVisibleFlag>,
                                        IApplicationSettingConsumer<CompanyFilter> {

    [Inject]
    public ApplicationStateFacade ApplicationStateFacade { get; set; }

    [Inject]
    public IState<EntityState<Company>> CompaniesState { get; set; }

    [Inject]
    public ModelFacade ModelFacade { get; set; }

    public List<string> CompanyName => CompaniesState.HasValue() ? CompaniesState.Value.Entities.Select(c => c.Name).ToList() : 
                                                                  new List<string>();

    private bool _isGroupedTogether = true;
    public bool IsGroupedTogether {
        get => _isGroupedTogether;
        set {
            ApplicationStateFacade.Set(new IsGroupedByCompanyFlag {
                PreviousValue = _isGroupedTogether,
                CurrentValue = value
            });
            _isGroupedTogether = value;
        }
    }

    private bool _isInFocusOnly = false;
    public bool IsInFocusOnly {
        get => _isInFocusOnly;
        set {
            ModelFacade.SetFilter(new IsInFocusedTskModelFilter { IsApplied = value });

            ApplicationStateFacade.Set(new IsInFocusOnlyFlag {
                PreviousValue = _isInFocusOnly,
                CurrentValue = value
            });

            _isInFocusOnly = value;
        }
    }

    private bool _isActionStatusOnly = false;
    public bool IsActionStatusOnly {
        get => _isActionStatusOnly;
        set {
            ApplicationStateFacade.Set(new IsActionStatusOnlyFlag {
                PreviousValue = _isActionStatusOnly,
                CurrentValue = value
            });
            _isActionStatusOnly = value;
        }
    }

    private bool _isTskQuickPaneVisible = false;
    public bool IsTskQuickPaneVisible {
        get => _isTskQuickPaneVisible;
        set {
            ApplicationStateFacade.Set(new IsTskQuickPaneVisibleFlag {
                PreviousValue = _isTskQuickPaneVisible,
                CurrentValue = value
            });
            _isTskQuickPaneVisible = value;
        }
    }

    private List<int> _companyFilter = new List<int>();
    public List<string> CompanyFilter {
        get => _companyFilter;
        set {
            ApplicationStateFacade.Set(new CompanyFilter {
                PreviousValue = _companyFilter,
                CurrentValue = value
            });
            _companyFilter = value;
        }
    }

    public string CompanyNames { get; set; }

    private string GetCompanyNameFilterText(List<string> selectedCompanyNames) {
        return string.Join(',', selectedCompanyNames);
        //return ""; // string.Join(',', Companies.Where(c => selectedValues.Contains(c.Id))); 
    }

    public Task ApplySetting(IsInFocusOnlyFlag applicationSetting) {
        _isInFocusOnly = applicationSetting.CurrentValue;
        return Task.CompletedTask;
    }

    public Task ApplySetting(IsGroupedByCompanyFlag applicationSetting) {
        _isGroupedTogether = applicationSetting.CurrentValue;
        return Task.CompletedTask;
    }

    public Task ApplySetting(IsActionStatusOnlyFlag applicationSetting) {
        _isActionStatusOnly = applicationSetting.CurrentValue;
        return Task.CompletedTask;
    }

    public Task ApplySetting(IsTskQuickPaneVisibleFlag applicationSetting) {
        _isTskQuickPaneVisible = applicationSetting.CurrentValue;
        return Task.CompletedTask;
    }

    public Task ApplySetting(CompanyFilter applicationSetting) {
        throw new System.NotImplementedException();
    }
}