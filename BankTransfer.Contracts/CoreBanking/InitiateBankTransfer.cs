using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.Contracts.CoreBanking
{
    public class InitiateBankTransfer
    {
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Narration { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryAccountName { get; set; }
        public string BeneficiaryBankCode { get; set; }
        public int MaxRetryAttempt { get; set; }
        public string CallBackUrl { get; set; }
        public string TransactionReference { get; set; }
    }
}
