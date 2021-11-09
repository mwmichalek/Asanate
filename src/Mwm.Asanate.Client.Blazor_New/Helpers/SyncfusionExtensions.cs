using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.Asanate.Client.Blazor.Helpers {
    public static class SyncfusionExtensions {

        public static List<string> ToKeyFields(this string keyField) {
            return new List<string> { keyField };
        }


    }
}
