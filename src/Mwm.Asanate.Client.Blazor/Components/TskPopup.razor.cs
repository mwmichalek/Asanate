using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Blazor.Models.Tsks;
using Mwm.Asanate.Client.Service.Facades;
using Mwm.Asanate.Client.Service.Store.Features.Shared.Helpers;
using Mwm.Asanate.Client.Service.Store.State.Shared;
using Mwm.Asanate.Application.Tsks.Commands;
using Mwm.Asanate.Domain;
using Syncfusion.Blazor.Popups;
using System;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Components {
    public partial class TskPopup : ComponentBase {

        [Inject]
        public ILogger<TskPopup> Logger { get; set; }

        [Inject]
        public EntityStateFacade EntityStateFacade { get; set; }

        [Inject]
        public IState<EntityState<Tsk>> TsksState { get; set; }

        [Parameter]
        public TskModel TskModel { get; set; }

        protected override async Task OnInitializedAsync() {
            Logger.LogInformation($"TskPop!!!!!");
            
            //await
                
        }

        public void DialogCloseHandler(CloseEventArgs args) {
            TskModel = null;
        }


        public async Task Save() {
            try {
                if (TskModel.Id == 0) {
                    Logger.LogInformation($"Add: {TskModel.Name}");
                } else {
                    var tsk = TsksState.FindById(TskModel.Id);
                    EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
                        Id = TskModel.Id,
                        Name = tsk.Name,
                        Status = TskModel.Status
                    });
                    Logger.LogInformation($"Update: {TskModel.Name}");
                }
                
            } catch (Exception ex) {
                Logger.LogError($"Unable to update: {TskModel.Name}, {ex}");
            }
        }

        public bool IsDialogShowing { get => TskModel != null; set => Console.WriteLine(value); }
    }
}
