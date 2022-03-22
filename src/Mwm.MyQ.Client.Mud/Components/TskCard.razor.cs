using Microsoft.AspNetCore.Components;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Models;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Domain;
using System;
using System.Threading.Tasks;
using Mwm.MyQ.Client.Service.Components;
using Mwm.MyQ.Client.Service.Store.Features.Settings;
using System.Collections.Generic;

namespace Mwm.MyQ.Client.Mud.Components;

public partial class TskCard : ModelConsumerComponent<TskModel, Tsk>,
                               IApplicationSettingConsumer<IsGroupedByCompanyFlag> {

    [Inject]
    public ModelFacade ModelFacade { get; set; }

    [Parameter]
    public TskModel TskModel { get; set; }

    public ActivityModel ActivityModel { get; set; } = new ActivityModel();

    public bool IncludeCompanyName { get; set; }

    public string HeaderStyle => TskModel.IsInFocus ? $"background-color: {TskModel.ProjectColor}; background-image: linear-gradient(rgba(0, 0, 0, 0.3) 0 0);" :
                                                      $"background-color: {TskModel.ProjectColor}; background-image: linear-gradient(rgba(0, 0, 0, 0.7) 0 0);";

    public string HeaderClasses => TskModel.IsInFocus ? "bg-primary" : "bg-dark";

    public string HourProgressDisplay {
        get {
            if (TskModel.DurationCompleted.HasValue && TskModel.DurationEstimate.HasValue) {
                if (TskModel.DurationCompleted.Value >= TskModel.DurationEstimate.Value)
                    return $"{TskModel.DurationCompleted.Value} / {TskModel.DurationCompleted.Value}";
                else
                    return $"{TskModel.DurationCompleted.Value} / {TskModel.DurationEstimate.Value}";
            } else if (TskModel.DurationCompleted.HasValue) {
                return $"{TskModel.DurationCompleted.Value}";
            } else if (TskModel.DurationEstimate.HasValue) {
                return $"~{TskModel.DurationEstimate.Value}";
            }
            return string.Empty;
        }
    }

    public string DueDateDisplay {
        get {
            var daysTillDueDate = DaysTillDueDate();
            return (daysTillDueDate.HasValue) ? $"{TskModel.DueDate.Value.ToString("MM/dd/yy")}" : string.Empty;
        }
    }

    public MudBlazor.Color DueDateCountColor {
        get {
            var daysTillDueDate = DaysTillDueDate();
            return (!daysTillDueDate.HasValue || daysTillDueDate > 0) ? MudBlazor.Color.Info :
                   (daysTillDueDate == 0) ? MudBlazor.Color.Warning :
                                            MudBlazor.Color.Error;
        }
    }

    public int? DueDateCount {
        get {
            var daysTillDueDate = DaysTillDueDate();
            return daysTillDueDate.HasValue ? Math.Abs(daysTillDueDate.Value) : null;
        } 
    }
        
    private int? DaysTillDueDate() {
        if (TskModel.DueDate.HasValue && !TskModel.CompletedDate.HasValue) {
            var daysTillDueDate = -1 * (int)(DateTime.Now - TskModel.DueDate.Value).TotalDays;
            return daysTillDueDate;
        }
        return null;
    }

    public bool IsInFocus {
        get {
            return TskModel.IsInFocus;
        }
        set {
            TskModel.IsInFocus = !TskModel.IsInFocus;
            Task.Run(() => EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
                Id = TskModel.Id,
                IsInFocus = TskModel.IsInFocus
            }));
        }
    }

    public bool IsDone {
        get {
            return TskModel.Status == Status.Done;
        }
        set {
            TskModel.Status = Status.Done;  
            Task.Run(() => EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
                Id = TskModel.Id,
                Status = TskModel.Status
            }));
        }
    }

    public bool IsArchived {
        get {
            return TskModel.IsArchived;
        }
        set {
            TskModel.IsArchived = !TskModel.IsArchived;
            Task.Run(() => EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
                Id = TskModel.Id,
                IsArchived = TskModel.IsArchived
            }));
        }
    }

    public async Task Edit() {
        await Task.Run(() => ModelFacade.Edit<TskModel, Tsk>(TskModel));
    }

    public Task ApplySetting(IsGroupedByCompanyFlag applicationSetting) {
        IncludeCompanyName = !applicationSetting.CurrentValue;
        return Task.CompletedTask;
    }

    private static bool _globalActivityFormIsVisible = false;

    private bool ActivityFormIsVisible { get; set; }

    public Task ShowActivityForm(bool isVisible) {
        if (isVisible && !_globalActivityFormIsVisible) {
            ActivityFormIsVisible = true;
            _globalActivityFormIsVisible = true;
        } else if (!isVisible) {
            ActivityFormIsVisible = false;
            _globalActivityFormIsVisible = false;
        }
        return Task.CompletedTask;
    }

    public async Task SaveActivity() {
        await Task.Run(() => EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
            Id = TskModel.Id,
            Activities = new List<Activity> {
                new Activity {
                    Notes = ActivityModel.Notes,
                    StartTime = ActivityModel.StartTime,
                    Duration = ActivityModel.Duration
                }
            }
        }));
        ActivityModel = new ActivityModel();
    }

}
