using System.Collections.Generic;

namespace MAD.DataWarehouse.BIM360.Api.DesignAutomation
{
    internal class WorkItem
    {
        public string Status { get; set; }
        public string Id { get; set; }
        public IDictionary<string, object> Stats { get; set; }
    }
}
