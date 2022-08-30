using Refit;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api.Project
{
    internal interface IProjectClient
    {
        [Get("/hubs")]
        Task<HubsResponse> Hubs();
    }
}
