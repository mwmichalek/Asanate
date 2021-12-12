using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Blayzor.Helpers {
    public static class SyncfusionExtensions {

        public static List<string> ToKeyFields(this string keyField) {
            return new List<string> { keyField };
        }


    }
}
