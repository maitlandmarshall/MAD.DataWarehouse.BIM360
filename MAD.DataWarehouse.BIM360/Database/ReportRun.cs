using System;
using System.Collections.Generic;

namespace MAD.DataWarehouse.BIM360.Database
{
    internal class ReportRun
    {
        public string WorkItemId { get; set; }

        public string ProjectId { get; set; }
        public string FolderItemId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Status { get; set; }
        public IDictionary<string, object> Stats { get; set; }
    }
}
