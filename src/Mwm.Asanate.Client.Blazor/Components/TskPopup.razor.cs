using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Blazor.Models.Tsks;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Components {
    public partial class TskPopup : ComponentBase {

        [Inject]
        public ILogger<TskPopup> Logger { get; set; }

        [Parameter]
        public TskModel TskModel { get; set; }

        protected override async Task OnInitializedAsync() {
            Logger.LogInformation($"TskPop!!!!!");
            
            //await
                
        }
    }
}
