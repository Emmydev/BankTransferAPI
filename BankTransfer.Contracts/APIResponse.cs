using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.Contracts
{
    public class APIResponse<T>
    {
        public APIResponse()
        {
            Error = new Error();
        }
        public bool IsSuccessful { get; set; }
        public T Result { get; set; }
        public Error Error { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
