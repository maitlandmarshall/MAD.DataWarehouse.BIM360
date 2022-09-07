using MAD.DataWarehouse.BIM360.Api.Project;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api.Data
{
    internal interface IDataClient
    {
        [Get("/projects/{projectId}/folders/{folderId}/contents")]
        Task<ApiResponse<FolderItem>> FolderContents(string projectId, string folderId);
    }
}
