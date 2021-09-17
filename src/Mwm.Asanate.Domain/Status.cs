using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {

    public enum Status {
        Unknown,
        Open,
        Planned,
        ReadyToStart,
        InProgress,
        Pending,
        Done
    }

    public static class StatusExtensions {

        private static Dictionary<string, Status> lookup = new Dictionary<string, Status> {
            { "Open", Status.Open },
            { "Queued", Status.Open },
            { "Planned", Status.Planned },
            { "Ready To Start", Status.ReadyToStart },
            { "In Progress", Status.InProgress },
            { "Pending", Status.Pending },
            { "Done", Status.Done },
        };

        public static Status ToStatus(this string statusStr) {
            return lookup.TryGetValue(statusStr, out Status status) ? status : Status.Unknown;
        }
    }

    //public class Status : NamedEntity {

    //    public Status(string value) {
    //        Value = value;
    //    }

    //    public string Value { get; private set; }

    //    public static Status Open => new Status("Open");
    //    public static Status Planned => new Status("Planned");

    //    // Chose not to include this.  Perhaps we should just make sure to many 
    //    // items reach the Planned state.  Leave them Open, the dumping ground.
    //    //public static Status Queued => new Status("Queued");

    //    public static Status ReadyToStart => new Status("Ready To Start");
    //    public static Status InProgress => new Status("In Progress");
    //    public static Status Pending => new Status("Pending");
    //    public static Status Done => new Status("Done");

    //    public static List<Status> Statuses = new List<Status> {
    //        Open,
    //        Planned,
    //        //Queued,
    //        ReadyToStart,
    //        InProgress,
    //        Pending,
    //        Done
    //    };
    //}

    //public static class StatusExtensions {

    //    public static Status ToStatus(this string statusName) {
    //        return Status.Statuses.SingleOrDefault(s => s.Value == statusName);
    //    }
    //}
}
