using Paystack.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paystack
{
    public interface IPaystackService
    {
        Task<BankList> GetPaystackBankList();
    }
}
