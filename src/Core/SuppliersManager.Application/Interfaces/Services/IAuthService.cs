using SuppliersManager.Application.Features.Auth.Commands;
using SuppliersManager.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuppliersManager.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<IResult<TokenResponse>> LoginJWT(TokenCommand command);
    }
}
