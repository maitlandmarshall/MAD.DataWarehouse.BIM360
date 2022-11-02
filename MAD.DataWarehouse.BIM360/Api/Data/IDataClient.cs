using Refit;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api.Data
{
    internal interface IDataClient
    {
        [Get("/projects/{projectId}/folders/{folderId}/contents")]
        Task<ApiResponse<FolderItem, FolderItem>> FolderContents(string projectId, string folderId);
    }
}
