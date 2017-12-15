using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceOptimizer.Model
{
    public class ChangedString
    {
        public int Index { get; set; }
        public string StrBefore { get; set; }
        public string StrAfter { get; set; }
        public string OldFileName { get; set; }
        public string NewFileName { get; set; }
    }
}
