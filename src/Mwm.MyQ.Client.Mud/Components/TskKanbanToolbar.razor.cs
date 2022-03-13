using Microsoft.AspNetCore.Components;
using Mwm.MyQ.Client.Service.Components;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Store.Features.ModelFilters;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Domain;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Mud.Components;

public partial class TskKanbanToolbar : ModelConsumerComponent<TskModel, Tsk>,
                                        IApplicationSettingConsumer<IsInFocusOnlyFlag>,
                                        IApplicationSettingConsumer<IsGroupedByCompanyFlag>,
                                        IApplicationSettingConsumer<IsActionStatusOnlyFlag>,
                                        IApplicationSettingConsumer<IsTskQuickPaneVisibleFlag> {

    [Inject]
    public ApplicationStateFacade ApplicationStateFacade { get; set; }

    [Inject]
    public ModelFacade ModelFacade { get; set; }

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
}