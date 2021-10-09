using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mwm.Asanate.Domain {

    public interface IEntity {

        public int Id { get; set; }

        public string? Gid { get; set; }

        
    }

    public class Entity : IEntity {

        [Key]
        public int Id { get; set; }

        public string? Gid { get; set; }

        
    }

}
