using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;

namespace Mwm.Asanate.Client.Blazor {
    public partial class App {

        private readonly IStore Store;

        public App(IStore store) {
            Store = store;
        }

        public void Run() {
            Console.Clear();
            Console.WriteLine("Initializing store");
            Store.InitializeAsync().Wait();
        }

     
    }
}
