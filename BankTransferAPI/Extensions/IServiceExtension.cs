namespace BankTransferAPI.Extensions
{
    public interface IServiceExtension
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
