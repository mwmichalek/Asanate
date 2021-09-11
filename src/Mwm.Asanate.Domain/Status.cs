using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {
    public class Status : NamedEntity {

        private Status() { }

        public string Value { get; private set; }

        public static Status Open => new Status { Value = "Open" };
        public static Status Planned => new Status { Value = "Planned" };
        public static Status ReadyToStart => new Status { Value = "Ready To Start" };
        public static Status InProgress => new Status { Value = "In Progress" };
        public static Status Pending => new Status { Value = "Pending" };
        public static Status Done => new Status { Value = "Done" };

        public static List<Status> Statuses = new List<Status> {
            Open,
            Planned,
            ReadyToStart,
            InProgress,
            Pending,
            Done
        };
    }

    public static class StatusExtensions {

        public static Status ToStatus(this string statusName) {
            return Status.Statuses.SingleOrDefault(s => s.Value == statusName);
        }
    }
}
