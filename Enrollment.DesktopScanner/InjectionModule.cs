using System.Reflection;
using Autofac;
using Enrollment.DesktopScanner.Services;
using Module = Autofac.Module;

namespace Enrollment.DesktopScanner
{
    internal class InjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            builder
                .RegisterAssemblyTypes(executingAssembly)
                .AsImplementedInterfaces()
                .AsSelf();

#if DEBUG
            builder
                .RegisterType<ScannerServiceStub>()
                .As<IScannerService>();
#else
            builder
                .RegisterType<ScannerService>()
                .As<IScannerService>();
#endif

        }
    }
}