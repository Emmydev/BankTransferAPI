using BankTransfer.DAL.Models;
using BankTransfer.DAL;
using Microsoft.EntityFrameworkCore;

namespace BankTransferAPI.Extensions
{
    public class DbExtension : IServiceExtension
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(option =>
                   option.UseSqlServer(
                       configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
        }
    }
}
