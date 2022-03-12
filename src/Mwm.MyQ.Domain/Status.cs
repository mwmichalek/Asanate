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

        public static List<Status> AllStatuses => new List<Status> { 
            Status.Open, 
            Status.Ready_To_Start, 
            Status.In_Progress,
            Status.Pending, 
            Status.Done 
        };

        public static List<Status> ActionStatuses => new List<Status> {
            Status.Ready_To_Start,
            Status.In_Progress,
            Status.Pending
        };



        public static Status ToStatus(this string statusStr) {
            if (Enum.TryParse(typeof(Status), statusStr.Replace(" ", "_"), true, out object? statusObj))
                return (Status)statusObj;
            if (statusStr == "Queued")
                return Status.Open;
            if (statusStr == "Planned")
                return Status.Open;
            return Status.Unknown;
        }
        public static string ToStr(this Status status) => status.ToString().Replace("_", " ");

    }

}
