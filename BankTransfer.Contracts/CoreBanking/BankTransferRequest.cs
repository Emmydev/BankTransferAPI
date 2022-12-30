using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.Contracts.CoreBanking
{
    public class BankTransferRequest
    {
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Narration { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryAccountName { get; set; }
        public string BeneficiaryBankCode { get; set; }
        public string CallBackUrl { get; set; }
        public int ProviderId { get; set; }
    }
}
