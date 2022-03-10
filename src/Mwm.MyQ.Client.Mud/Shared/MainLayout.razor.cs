using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Mwm.MyQ.Client.Mud;
using Mwm.MyQ.Client.Mud.Shared;
using MudBlazor;

namespace Mwm.MyQ.Client.Mud.Shared;

public partial class MainLayout {
    bool _drawerOpen = true;
    void DrawerToggle() {
        _drawerOpen = !_drawerOpen;
    }

    public MudTheme CustomTheme => new MudTheme() {
        Typography = new Typography() {
            H5 = new H5() {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                FontSize = "1.25rem",
                FontWeight = 500,
                LineHeight = 1.6,
                LetterSpacing = ".0075em"
            },
            H6 = new H6() {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                FontSize = ".9rem",
                FontWeight = 500,
                LineHeight = 1.6,
                LetterSpacing = ".0075em"
            }
        }
    };
}
