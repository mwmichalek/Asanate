﻿using Mwm.MyQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mwm.MyQ.Client.Service.Models;

public abstract class EntityModel<TNamedEntity> where TNamedEntity : INamedEntity {

    public int Id { get; set; }

    public string Name { get; set; }

}
