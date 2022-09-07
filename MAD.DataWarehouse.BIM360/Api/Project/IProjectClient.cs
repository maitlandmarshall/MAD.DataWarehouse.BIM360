using MAD.DataWarehouse.BIM360.Api.Data;
using Refit;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api.Project
{
    internal interface IProjectClient
    {
        [Get("/hubs")]
        Task<HubsResponse> Hubs();

        [Get("/hubs/{hubId}/projects/{projectId}/topFolders")]
        Task<ApiResponse<FolderItem>> TopFolders(string hubId, string projectId);
    }
}
