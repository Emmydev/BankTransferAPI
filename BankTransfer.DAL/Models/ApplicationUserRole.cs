using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.DAL.Models
{
    public class ApplicationUserRole: IdentityUserRole<Guid>
    {
    }
}
