using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceOptimizer.Model
{
    public class ResourceDescription
    {
        public int ResourceId { get; set; }

        public string ConstantName { get; set; }

        public string OriginalConstantName { get; set; }

        public string Value { get; set; }

        public string ConstatToEquls { get; set; }

        public string OriginalFileName { get; set; }
    }
}
