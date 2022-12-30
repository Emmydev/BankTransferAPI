using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.Contracts.Authentication
{
    public class AuthDetails
    {
        public string Token { get; set; }
        public string Expires { get; set; }
    }
}
