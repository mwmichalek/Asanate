using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mwm.MyQ.Domain;

public class Activity : Entity {

    public string Notes { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? StartTime { get; set; } 

    public DateTime? EndTime { get; set; }

    public float Duration { get; set; }

    public int TskId { get; set; }

    [JsonIgnore]
    public virtual Tsk Tsk { get; set; }

}
