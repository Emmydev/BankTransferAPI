using BankTransfer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.Contracts.Providers
{
    public class SelectProviders
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public SelectProviders(Provider provider)
        {
            ProviderId = provider.ProviderId;
            Name = provider.Name;
        }
    }
}
