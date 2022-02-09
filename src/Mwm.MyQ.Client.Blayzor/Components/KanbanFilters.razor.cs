﻿using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Actions;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Data;
using Mwm.MyQ.Domain;
using Syncfusion.Blazor.Kanban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Popups;
using Mwm.MyQ.Client.Blayzor.Components;
using Mwm.MyQ.Client.Blayzor.Helpers;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using Mwm.MyQ.Client.Service.Store.Features.ModelFilters;
using Mwm.MyQ.Client.Service.Components;

namespace Mwm.MyQ.Client.Blayzor.Components {
    public partial class KanbanFilters : ModelConsumerComponent<TskModel, Tsk>,
                                         IApplicationSettingConsumer<IsInFocusOnlyFlag>,
                                         IApplicationSettingConsumer<IsGroupedByCompanyFlag>,
                                         IApplicationSettingConsumer<IsActionStatusOnlyFlag> { 

        [Inject]
        public ApplicationStateFacade ApplicationStateFacade { get; set; }

        [Inject]
        public ModelFilterFacade ModelFilterFacade { get; set; }

        protected Task HandleUpdateAsync(IsGroupedByCompanyFlag flag) {
            _isGroupedTogether = flag.CurrentValue;
            return Task.CompletedTask;
        }

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

        protected Task HandleUpdateAsync(IsInFocusOnlyFlag flag) {
            _isInFocusOnly = flag.CurrentValue;
            return Task.CompletedTask;
        }

        private bool _isInFocusOnly = false;
        public bool IsInFocusOnly { 
            get => _isInFocusOnly;
            set {
                ApplicationStateFacade.Set(new IsInFocusOnlyFlag {
                    PreviousValue = _isInFocusOnly,
                    CurrentValue = value
                });

                ModelFilterFacade.Set(new IsInFocusedTskModelFilter { IsApplied = value });

                _isInFocusOnly = value;
            }
        }

        protected Task HandleUpdateAsync(IsActionStatusOnlyFlag flag) {
            _isActionStatusOnly = flag.CurrentValue;
            return Task.CompletedTask;
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

        public Task ApplySetting(IsInFocusOnlyFlag applicationSetting) {
            Logger.LogInformation($"IsInFocusOnlyFlag: {applicationSetting.CurrentValue}");
            return Task.CompletedTask;
        }

        public Task ApplySetting(IsGroupedByCompanyFlag applicationSetting) {
            Logger.LogInformation($"IsGroupedByCompanyFlag: {applicationSetting.CurrentValue}");
            return Task.CompletedTask;
        }

        public Task ApplySetting(IsActionStatusOnlyFlag applicationSetting) {
            Logger.LogInformation($"IsActionStatusOnlyFlag: {applicationSetting.CurrentValue}");
            return Task.CompletedTask;
        }

    }
}