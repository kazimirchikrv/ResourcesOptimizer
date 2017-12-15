using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceOptimizer.Model
{
    public class InvestigationFile : ResourceFile
    {
        public StringBuilder WholeFileBeforeSchanges { get; set; }
        public int CountResourceFiles { get; set; }
        public StringBuilder WholeFileAfterChanges { get; set; }
        public List<string> ListChanges { get; set; } = new List<string>();
    }
}
