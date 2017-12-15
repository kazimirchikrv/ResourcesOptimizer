using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceOptimizer.Model
{
    public class ResourceFile
    {
        public int ResourceId { get; set; }

        public string FileName { get; set; }

        public string PathToFile { get; set; }

        public virtual List<ResourceDescription> InternalResourses { get; set; }
    }
}
