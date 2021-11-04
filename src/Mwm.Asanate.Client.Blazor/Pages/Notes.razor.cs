using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.Asanate.Client.Blazor.Models.Tsks;
using Mwm.Asanate.Application.Tsks.Commands;
using Mwm.Asanate.Client.Service.Facades;
using System;
using Mwm.Asanate.Domain;
using Mwm.Asanate.Client.Service.Store.State.Shared;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;

namespace Mwm.Asanate.Client.Blazor.Pages {
    public partial class Notes : FluxorComponent {

        [Inject]
        ILogger<Notes> Logger { get; set; }

        [Inject]
        public IState<EntityState<Tsk>> TsksState { get; set; }

        [Inject]
        public EntityStateFacade EntityStateFacade { get; set; }

        public TskModel NewTskModel { get; set; } = new TskModel();

        public void Save() {
            Logger.LogInformation($"Saving Popup.");
            try {

                Logger.LogInformation($"Add: {NewTskModel.Name}");

                EntityStateFacade.Add<Tsk, TskAdd.Command>(new TskAdd.Command {
                    Name = NewTskModel.Name,
                    DurationEstimate = NewTskModel.DurationEstimate
                });
            } catch (Exception ex) {
                Logger.LogError($"Unable to update: {NewTskModel.Name}, {ex}");
            }
        }

        public void KeyboardEventHandler(KeyboardEventArgs args) {
            if (args.Key == "Enter")
                Save();
        }
    }
}
