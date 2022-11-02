using System.Collections.Generic;

namespace MAD.DataWarehouse.BIM360.Api
{
    internal class ApiResponse<TData>
    {
        public IEnumerable<TData> Data { get; set; }
    }
    
    internal class ApiResponse<TData, TIncluded>
    {
        public IEnumerable<TData> Data { get; set; }
        public IEnumerable<TIncluded> Included { get; set; }
    }
}
