using Koton.ECommerce.Core.Common;
using Koton.ECommerce.Core.DTOs;

namespace Koton.ECommerce.Business.Services.Abstract
{
    public interface ILoginService
    {
        Task<Result<LoginResultDto>> LoginAsync(string username, string password);
    }
}
