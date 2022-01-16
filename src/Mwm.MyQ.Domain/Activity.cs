using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Domain;

public class Activity : Entity {

    public string Notes { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? StartTime { get; set; } 

    public DateTime? EndTime { get; set; }

    public float Duration { get; set; }

    public int TskId { get; set; }

    public Tsk Tsk { get; set; }

}
