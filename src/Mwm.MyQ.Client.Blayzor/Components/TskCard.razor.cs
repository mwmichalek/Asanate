using Microsoft.AspNetCore.Components;
using Mwm.MyQ.Client.Blayzor.Models.Tsks;

namespace Mwm.MyQ.Client.Blayzor.Components;

public partial class TskCard : ComponentBase {

    [Parameter]
    public TskModel TskModel { get; set; }

    public string HourProgressDisplay =>
             (TskModel.DurationCompleted.HasValue && TskModel.DurationEstimate.HasValue) ? $"{TskModel.DurationCompleted} / ~{TskModel.DurationEstimate}" :
                    TskModel.DurationCompleted.HasValue ? TskModel.DurationCompleted.ToString() :
                    TskModel.DurationEstimate.HasValue ? $"~{TskModel.DurationEstimate}" : "";

    public string DueDateDisplay =>
        (TskModel.DueDate.HasValue && !TskModel.CompletedDate.HasValue) ? TskModel.DueDate.Value.ToString("MM/dd/yyyy") : string.Empty;
}
