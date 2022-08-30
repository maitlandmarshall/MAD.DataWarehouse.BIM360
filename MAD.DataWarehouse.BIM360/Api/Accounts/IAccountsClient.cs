using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.BIM360.Api.Accounts
{
    internal interface IAccountsClient
    {
        [Get("/accounts/{accountId}/projects")]
        Task<IEnumerable<Project>> Projects(string accountId, [Query] int limit = 100, [Query] int offset = 0, [Query] string sort = null, [Query] string field = null);
    }
}
