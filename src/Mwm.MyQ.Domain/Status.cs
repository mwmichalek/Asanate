using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Domain {

    public enum Status {
        Unknown,
        Open,
        Planned,
        Ready_To_Start,
        In_Progress,
        Pending,
        Done
    }

  
    public static class StatusExtensions {

        public static List<Status> All => new List<Status> { 
            Status.Open, 
            Status.Planned, 
            Status.Ready_To_Start, 
            Status.Pending, 
            Status.Done 
        };

        public static Status ToStatus(this string statusStr) {
            if (Enum.TryParse(typeof(Status), statusStr.Replace(" ", "_"), true, out object? statusObj))
                return (Status)statusObj;
            if (statusStr == "Queued")
                return Status.Open;
            return Status.Unknown;
        }
        public static string ToStr(this Status status) => status.ToString().Replace("_", " ");

    }

}
