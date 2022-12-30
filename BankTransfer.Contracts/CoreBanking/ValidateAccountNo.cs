using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.Contracts.CoreBanking
{
    public class ValidateAccountNo
    {
        public string Code { get; set; }
        public string AccountNumber { get; set; }
        public int ProviderId { get; set; }
    }
}
