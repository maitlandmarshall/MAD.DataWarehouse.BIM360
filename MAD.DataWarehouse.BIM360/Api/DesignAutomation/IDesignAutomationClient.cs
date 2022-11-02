using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api.DesignAutomation
{
    internal interface IDesignAutomationClient
    {
        [Post("/workitems")]
        Task<WorkItem> CreateWorkItem([Body] object payload);
    }
}
