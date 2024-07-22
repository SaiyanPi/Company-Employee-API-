using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
    public class Link
    {
        public string? Href { get; set; } // defines the URI to the action
        public string? Rel { get; set; } // defines the identification of the action
        public string? Method { get; set; } // defines which HTTP method should be used for that action
        public Link()
        { }
        public Link(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
