using Microsoft.AspNetCore.Components;
using Mwm.MyQ.Client.Blayzor.Models.Tsks;
using System;

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class TskCard : ComponentBase {

    [Parameter]
    public TskModel TskModel { get; set; }

    public string HourProgressDisplay =>
             (TskModel.DurationCompleted.HasValue && TskModel.DurationEstimate.HasValue) ? $"{TskModel.DurationCompleted} / ~{TskModel.DurationEstimate}" :
                    TskModel.DurationCompleted.HasValue ? TskModel.DurationCompleted.ToString() :
                    TskModel.DurationEstimate.HasValue ? $"~{TskModel.DurationEstimate}" : "";

    public string DueDateDisplay {
        get {
            var daysTillDueDate = DaysTillDueDate();
            return (daysTillDueDate.HasValue) ? $"{TskModel.DueDate.Value.ToString("MM/dd/yyyy")} ({Math.Abs(daysTillDueDate.Value)})" : string.Empty;
        } 
    }
    public string DueDateDisplayClass {
        get {
            var daysTillDueDate = DaysTillDueDate();
            return (!daysTillDueDate.HasValue) ? string.Empty :
                   (daysTillDueDate > 0) ?  "text-light" : 
                   (daysTillDueDate == 0) ? "text-warning" : 
                                            "text-danger";
        }
    }

    public int? DaysTillDueDate() {
        if (TskModel.DueDate.HasValue && !TskModel.CompletedDate.HasValue) {
            var daysTillDueDate = -1 * (int)(DateTime.Now - TskModel.DueDate.Value).TotalDays;
            return daysTillDueDate;
        }
        return null;
    }
}
