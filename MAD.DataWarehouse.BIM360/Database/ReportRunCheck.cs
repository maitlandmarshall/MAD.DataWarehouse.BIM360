using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Database
{
    internal class ReportRunCheck
    {
        public string WorkItemId { get; set; }
        public string CheckId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }
        public string FailureMessage { get; set; }
        public string ResultMessage { get; set; }
        public string Error { get; set; }
        
        public int Count { get; set; }
        public int Index { get; set; }
    }
}
