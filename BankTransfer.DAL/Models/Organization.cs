using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.DAL.Models
{
    public class Organization: CommonEntity
    {
        [Key]
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string ClientSecret { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
