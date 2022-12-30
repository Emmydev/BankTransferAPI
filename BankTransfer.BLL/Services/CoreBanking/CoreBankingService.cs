using Azure;
using Azure.Core;
using BankTransfer.BLL.Constants;
using BankTransfer.Contracts;
using BankTransfer.Contracts.Authentication;
using BankTransfer.Contracts.CoreBanking;
using BankTransfer.Contracts.Providers;
using BankTransfer.DAL;
using BankTransfer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Paystack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.BLL.Services.CoreBanking
{
    public class CoreBankingService : ICoreBankingService
    {
        private readonly DataContext context;
        private readonly IPaystackService paystackService;

        public CoreBankingService(DataContext context, IPaystackService paystackService)
        {
            this.context = context;
            this.paystackService = paystackService;
        }

        public async Task<APIResponse<List<BankList>>> GetBankList(int providerId)
        {
            var response = new APIResponse<List<BankList>>();
            try
            {
                var providers = context.Provider.Where(x => x.ProviderId == providerId && x.Deleted != true);
                if (providers == null)
                {
                    response.IsSuccessful = false;
                    response.Error.Code = Codes.NotFound;
                    response.Error.Description = "ProviderId doesn't exist";
                    return response;
                }
                var bankList = await GetActualBankList(providerId);
                response.IsSuccessful = true;
                response.Result = bankList;
                return response;
            }
            catch (Exception ex)
            {
                ///>> log error
                response.IsSuccessful = false;
                response.Error.Code = Codes.SystemMalfunction;
                response.Error.Description = Messages.FriendlyException;
                return response;
            }
        }

        public async Task<APIResponse<List<SelectProviders>>> GetProviders()
        {
            var response = new APIResponse<List<SelectProviders>> ();
            try
            {
                var providers = context.Provider.Where(x => x.Deleted != true);
                if (providers == null)
                {
                    response.IsSuccessful = false;
                    response.Error.Code = Codes.NotFound;
                    response.Error.Description = Messages.FriendlyNOTFOUND;
                    return response;
                }
                response.IsSuccessful = true;
                response.Result = await providers.Select(provider => new SelectProviders(provider)).ToListAsync();
                return response;
            }
            catch(Exception ex)
            {
                ///>> log error
                response.IsSuccessful = false;
                response.Error.Code = Codes.SystemMalfunction;
                response.Error.Description = Messages.FriendlyException;
                return response;
            }
        }

        public async Task<APIResponse<AccountNoResponse>> ValidateAccountNumber(ValidateAccountNo request)
        {
            var response = new APIResponse<AccountNoResponse>();
            try
            {
                var providers = context.Provider.Where(x => x.ProviderId == request.ProviderId && x.Deleted != true);
                if (providers == null)
                {
                    response.IsSuccessful = false;
                    response.Error.Code = Codes.NotFound;
                    response.Error.Description = "ProviderId doesn't exist";
                    return response;
                }
                var accountDetails = await GetAccountDetails(request);
                response.IsSuccessful = true;
                response.Result = accountDetails;
                return response;
            }
            catch (Exception ex)
            {
                ///>> log error
                response.IsSuccessful = false;
                response.Error.Code = Codes.SystemMalfunction;
                response.Error.Description = Messages.FriendlyException;
                return response;
            }
        }

        public async Task<APIResponse<BankTransferResponse>> BankTransfer(BankTransferRequest request)
        {
            var response = new APIResponse<BankTransferResponse>();
            try
            {
                var providers = context.Provider.Where(x => x.ProviderId == request.ProviderId && x.Deleted != true);
                if (providers == null)
                {
                    response.IsSuccessful = false;
                    response.Error.Code = Codes.NotFound;
                    response.Error.Description = "ProviderId doesn't exist";
                    return response;
                }

                var transaction = await context.Transaction.Where(x => x.BeneficiaryAccountNumber == request.BeneficiaryAccountNumber &&
                                    x.BeneficiaryBankCode == request.BeneficiaryBankCode &&
                                    x.ProviderId == request.ProviderId &&
                                    x.Amount == request.Amount && x.CallBackUrl.ToLower() == request.CallBackUrl.ToLower() && 
                                    x.Narration.ToLower() == x.Narration.ToLower()).FirstOrDefaultAsync();
                
                string transactionRef;
                if(transaction == null)
                {
                    transactionRef = await GenerateTransactionReference();
                }
                else
                {
                    transactionRef = transaction.TransactionReference;
                }

                var result = await InitiateBankTransfer(request, transactionRef);
                response.IsSuccessful = true;
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                ///>> log error
                response.IsSuccessful = false;
                response.Error.Code = Codes.SystemMalfunction;
                response.Error.Description = Messages.FriendlyException;
                return response;
            }
        }


        public async Task<APIResponse<TransactionStatus>> TransactionStatus(string transactionReference)
        {
            var response = new APIResponse<TransactionStatus>();
            try
            {
                var transaction = await context.Transaction.Where(x => x.TransactionReference == transactionReference).FirstOrDefaultAsync();
                if(transaction == null)
                {
                    response.IsSuccessful = false;
                    response.Error.Code = Codes.NotFound;
                    response.Error.Description = "TransactionReference doesn't exist";
                    return response;
                }
                var transactionStatus = await GetTransactionStatus(transactionReference, transaction.ProviderId);
                response.IsSuccessful = true;
                response.Result = transactionStatus;
                return response;
            }
            catch (Exception ex)
            {
                ///>> log error
                response.IsSuccessful = false;
                response.Error.Code = Codes.SystemMalfunction;
                response.Error.Description = Messages.FriendlyException;
                return response;
            }
        }

        private async Task<List<BankList>> GetActualBankList(int providerId)
        {
            if(providerId == (int)Providers.Default || providerId == (int)Providers.Paystack)
            {
                var paystackList = await paystackService.GetPaystackBankList();
                if(paystackList == null)
                {
                    return null;
                }
                if(paystackList.Status)
                {
                    var bankList = paystackList.Data.Select(x => new BankList
                    {
                        Code = x.Code,
                        BankName = x.Name,
                        LongCode = x.Longcode
                    }).ToList();
                    return bankList;
                }
            }
            else if(providerId == (int)Providers.Flutterwave)
            {
               ///>>> Call Flutterwave service to retrieve bank list
            }
            return null;
        }
        private async Task<AccountNoResponse> GetAccountDetails(ValidateAccountNo request)
        {
            if (request.ProviderId == (int)Providers.Default || request.ProviderId == (int)Providers.Paystack)
            {
                ///>>>> Call Paystack Service to validate account number
            }
            else if (request.ProviderId == (int)Providers.Flutterwave)
            {
                ///>>> Call Flutterwave service to retrieve bank list
            }
            return null;
        }
        private async Task<BankTransferResponse> InitiateBankTransfer(BankTransferRequest request, string transactionRef)
        {
            var bankTransfer = new InitiateBankTransfer
            {
                BeneficiaryAccountName = request.BeneficiaryAccountName,
                CurrencyCode = request.CurrencyCode,
                Narration = request.Narration,
                BeneficiaryAccountNumber = request.BeneficiaryAccountNumber,
                BeneficiaryBankCode = request.BeneficiaryBankCode,
                CallBackUrl = request.CallBackUrl,
                TransactionReference = transactionRef,
                Amount = request.Amount
            };
            if (request.ProviderId == (int)Providers.Default || request.ProviderId == (int)Providers.Paystack)
            {
                ///>>> Call Paystack Service to intiate bank transfer
            }
            else if (request.ProviderId == (int)Providers.Flutterwave)
            {
                ///>>> Call Flutterwave service to intiate bank transfer
            }
            return null;
        }
        private async Task<string> GenerateTransactionReference()
        {
            ///>>> Logic to generate transaction reference
            throw new NotImplementedException();
        }
        private async Task<TransactionStatus> GetTransactionStatus(string transactionReference, int providerId)
        {
            if (providerId == (int)Providers.Default || providerId == (int)Providers.Paystack)
            {
                ///>>> Call Paystack Service to intiate bank transfer
            }
            else if (providerId == (int)Providers.Flutterwave)
            {
                ///>>> Call Flutterwave service to intiate bank transfer
            }
            return null;
        }
    }
}
