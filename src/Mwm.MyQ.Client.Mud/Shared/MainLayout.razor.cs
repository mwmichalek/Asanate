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

    public void DrawOpen() {
        IsDrawOpen = true;
    }

    public void DrawClose() {
        IsDrawOpen = false;
    }

    public bool IsDrawOpen { get; set; }

    public MudTheme CustomTheme => new MudTheme() {
        Typography = new Typography() {
            H1 = new H1() {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                FontSize = "4rem",
                FontWeight = 500,
                LineHeight = 1.6,
                LetterSpacing = ".0075em"
            },
            H2 = new H2() {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                FontSize = "2.5rem",
                FontWeight = 500,
                LineHeight = 1.6,
                LetterSpacing = ".0075em"
            },
            H3 = new H3() {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                FontSize = "2rem",
                FontWeight = 500,
                LineHeight = 1.6,
                LetterSpacing = ".0075em"
            },
            H4 = new H4() {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                FontSize = "1.25rem",
                FontWeight = 500,
                LineHeight = 1.6,
                LetterSpacing = ".0075em"
            },
            H5 = new H5() {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                FontSize = ".9rem",
                FontWeight = 500,
                LineHeight = 1.6,
                LetterSpacing = ".0075em"
            },
            H6 = new H6() {
                FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                FontSize = ".75rem",
                FontWeight = 500,
                LineHeight = 1.6,
                LetterSpacing = ".0075em"
            }
        }
    };
}
