using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.Contracts.Authentication
{
    public class LoginDetails
    {
        public int ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
