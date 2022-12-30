using BankTransfer.Contracts;
using BankTransfer.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.BLL.Services.Authentication
{
    public interface IIdentityService
    {
        Task<APIResponse<AuthDetails>> Login(LoginDetails request);
    }
}
