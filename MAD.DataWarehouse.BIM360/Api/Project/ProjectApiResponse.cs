using System.Collections.Generic;

namespace MAD.DataWarehouse.BIM360.Api.Project
{
    internal class ProjectApiResponse<TData>
    {
        public IEnumerable<TData> Data { get; set; }
    }
}
