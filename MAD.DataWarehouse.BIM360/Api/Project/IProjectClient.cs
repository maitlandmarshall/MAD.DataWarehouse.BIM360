using Refit;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api.Project
{
    internal interface IProjectClient
    {
        [Get("/hubs")]
        Task<HubsResponse> Hubs();

        [Get("/hubs/{hubId}/projects/{projectId}/topFolders")]
        Task<ProjectApiResponse<FolderItem>> TopFolders(string hubId, string projectId);

        [Get("/hubs/{hubId}/projects/{projectId}/folders/{folderId}/contents")]
        Task<ProjectApiResponse<FolderItem>> FolderContents(string hubId, string projectId, string folderId);
    }
}
