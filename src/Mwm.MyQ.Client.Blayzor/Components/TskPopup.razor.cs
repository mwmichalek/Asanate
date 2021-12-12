using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Mwm.MyQ.Client.Blayzor.Models.Tsks;
using Mwm.MyQ.Client.Service.Facades;
using Mwm.MyQ.Client.Service.Store.Features.Shared.Helpers;
using Mwm.MyQ.Client.Service.Store.State.Shared;
using Mwm.MyQ.Application.Tsks.Commands;
using Mwm.MyQ.Domain;
using Syncfusion.Blazor.Popups;
using System;
using System.Threading.Tasks;
using Syncfusion.Blazor.RichTextEditor;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace Mwm.MyQ.Client.Blayzor.Components {
    public partial class TskPopup : ComponentBase {

        public SfDialog Dialog { get; set; }

        public SfRichTextEditor TextEditor { get; set; }

        [Inject]
        public ILogger<TskPopup> Logger { get; set; }

        [Inject]
        public EntityStateFacade EntityStateFacade { get; set; }

        [Inject]
        public IState<EntityState<Tsk>> TsksState { get; set; }

        public bool IsDialogShowing { get; set; }

        public bool IsNew => TskModel != null && TskModel.Id == 0;

        public TskModel TskModel;

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (TextEditor != null) 
                await TextEditor?.RefreshUIAsync();
            await base.OnAfterRenderAsync(firstRender);
        }

        public void Add(int? initiativeId) {
            Logger.LogInformation($"Adding TskModel.");
            TskModel = new TskModel {
                InitiativeId = initiativeId
            };
            IsDialogShowing = true;
            StateHasChanged();
        }

        public void Update(TskModel tskModel) {
            Logger.LogInformation($"Updating TskModel.");
            TskModel = tskModel;
            IsDialogShowing = true;
            StateHasChanged();
        }

        public void Close() {
            Logger.LogInformation($"Closing TskModel.");
            TskModel = null;
            
            IsDialogShowing = false;
            StateHasChanged();
        }

        protected override Task OnInitializedAsync() {
            Logger.LogInformation($"Initializing Popup.");   
            return base.OnInitializedAsync();
        }

        public void DialogCloseHandler(CloseEventArgs args) {
            Logger.LogInformation($"Closing Popup.");
            Close();
        }

        public void KeyboardEventHandler(KeyboardEventArgs args) {
            if (args.Key == "Enter")
                Save();
        }

        public void Save() {
            Logger.LogInformation($"Saving Popup.");
            try {
                if (TskModel.Id == 0) {
                    Logger.LogInformation($"Add: {TskModel.Name}");

                    EntityStateFacade.Add<Tsk, TskAdd.Command>(new TskAdd.Command {
                        Name = TskModel.Name,
                        Status = TskModel.Status,
                        DurationEstimate = TskModel.DurationEstimate,
                        DurationCompleted = TskModel.DurationCompleted,
                        Notes = TskModel.Notes,
                        DueDate = TskModel.DueDate,
                        StartDate = TskModel.StartDate,
                        StartedDate = TskModel.StartedDate,
                        CompletedDate = TskModel.CompletedDate
                    });

                } else {
                    var tsk = TsksState.FindById(TskModel.Id);
                    EntityStateFacade.Update<Tsk, TskUpdate.Command>(new TskUpdate.Command {
                        Id = TskModel.Id,
                        Name = TskModel.Name,
                        Status = TskModel.Status,
                        DurationEstimate = TskModel.DurationEstimate,
                        DurationCompleted = TskModel.DurationCompleted, 
                        Notes = TskModel.Notes,
                        DueDate = TskModel.DueDate, 
                        StartDate = TskModel.StartDate, 
                        StartedDate = TskModel.StartedDate, 
                        CompletedDate = TskModel.CompletedDate
                    });
                    Logger.LogInformation($"Update: {TskModel.Name}");
                }
                Close();
            } catch (Exception ex) {
                Logger.LogError($"Unable to update: {TskModel.Name}, {ex}");
            }
        }

        private static List<float> _durationDropDownValues = new List<float> { 0.25f, .5f, 1, 2, 3, 4, 5, 6, 7, 8 };

        public List<float> DurationDropDown => _durationDropDownValues;
    }
}
