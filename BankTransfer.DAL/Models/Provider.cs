using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.DAL.Models
{
    public class Provider: CommonEntity
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
    }
}
