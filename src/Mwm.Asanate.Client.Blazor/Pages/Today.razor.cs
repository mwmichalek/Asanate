using Microsoft.AspNetCore.Components;
using Mwm.Asanate.Data;
using Mwm.Asanate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Pages {
    public partial class Today {

        [Inject]
        public DatabaseContext DatabaseContext { get; set; }

        public List<Tsk> Tsks { get; set; }

        protected override async Task OnInitializedAsync() {
            Tsks = DatabaseContext.Tsks.ToList();
        }
    }
}
