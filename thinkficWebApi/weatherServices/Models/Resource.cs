using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weatherServices.Models
{
    public abstract class Resource
    {
        public string Href { get; set; } = "Resource";
    }
}
