﻿using BudgetTracking.Common.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Common.Entity
{
    public class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
