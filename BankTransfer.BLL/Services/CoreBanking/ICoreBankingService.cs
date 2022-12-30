using BankTransfer.Contracts;
using BankTransfer.Contracts.CoreBanking;
using BankTransfer.Contracts.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.BLL.Services.CoreBanking
{
    public interface ICoreBankingService
    {
        Task<APIResponse<List<SelectProviders>>> GetProviders();
        Task<APIResponse<List<BankList>>> GetBankList(int providerId);
        Task<APIResponse<AccountNoResponse>> ValidateAccountNumber(ValidateAccountNo request);
        Task<APIResponse<BankTransferResponse>> BankTransfer(BankTransferRequest request);
        Task<APIResponse<TransactionStatus>> TransactionStatus(string transactionReference);

    }
}
