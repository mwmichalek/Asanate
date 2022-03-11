using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Mwm.MyQ.Client.Mud.Pages;
using System;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Mud.Shared;

public partial class TopNav {

    [Inject]
    NavigationManager NavigationManager { get; set; }

    public Page CurrentPage { get; set; } = Page.Kanban;

    protected override Task OnInitializedAsync() {
        NavigationManager.LocationChanged += LocationChanged;
        return base.OnInitializedAsync();
    }

    void LocationChanged(object sender, LocationChangedEventArgs e) {
        Page newPage = Page.Kanban; // Default
        foreach (var pageName in Enum.GetNames(typeof(Page))) {
            if (e.Location.ToLower().Contains(pageName.ToLower())) {
                newPage = (Page)Enum.Parse(typeof(Page), pageName);
                break;
            }
        }

        if (CurrentPage != newPage) {
            CurrentPage = newPage;
            StateHasChanged();
        }
    }

}
