﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FlowerTag> FlowerTags { get; set; }
    }
}
