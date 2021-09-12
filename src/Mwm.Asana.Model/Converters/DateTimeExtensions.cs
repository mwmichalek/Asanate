using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asana.Model.Converters {
    public static class DateTimeExtensions {

        public static string ToUtcIsoDateString(this DateTime dt) {
            return dt.ToUniversalTime().ToIsoDateString();
        }

        public static string ToIsoDateString(this DateTime dt) {
            return dt.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public static DateTime? ToDateTime(this string dateTimeString) {
            if (DateTime.TryParse(dateTimeString, out DateTime dateTime))
                return dateTime;
            return null;
        }
    }
}
