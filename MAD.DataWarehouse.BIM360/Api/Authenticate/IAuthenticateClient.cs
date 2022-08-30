using Refit;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api.Authenticate
{
    internal interface IAuthenticateClient
    {
        [Post("/authenticate")]
        [Headers("Content-Type:application/x-www-form-urlencoded")]
        Task<AuthenticateResponse> Authenticate([Body(BodySerializationMethod.UrlEncoded)] AuthenticateRequest request);
    }
}
