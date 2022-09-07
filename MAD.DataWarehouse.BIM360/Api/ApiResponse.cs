﻿using System.Collections.Generic;

namespace MAD.DataWarehouse.BIM360.Api
{
    internal class ApiResponse<TData>
    {
        public IEnumerable<TData> Data { get; set; }
    }
}