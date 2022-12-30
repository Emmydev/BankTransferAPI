namespace BankTransferAPI.Extensions
{
    public static class InstallerExtension
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Program).Assembly.ExportedTypes.Where(x =>
               typeof(IServiceExtension).IsAssignableFrom(x) && !x.IsAbstract).ToList();

            var instanceOfInstallers = installers.Select(Activator.CreateInstance).Cast<IServiceExtension>().ToList();

            instanceOfInstallers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
