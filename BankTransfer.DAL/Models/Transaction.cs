using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.DAL.Models
{
    public class Transaction: CommonEntity
    {
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Narration { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryAccountName { get; set; }
        public string BeneficiaryBankCode { get; set; }
        public int MaxRetryAttempt { get; set; }
        public string CallBackUrl { get; set; }
        public int ProviderId { get; set; }
        public string TransactionReference { get; set; }
    }
}
