using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model.Converters {
    public static class DateTimeExtensions {

        public static string ToUtcIsoDate(this DateTime dt) {
            return dt.ToUniversalTime().ToIsoDate();
        }

        public static string ToIsoDate(this DateTime dt) {
            return dt.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
