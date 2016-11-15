using System;
using System.Reflection;
using Autofac;
using Enrollment.Scanner.Components.MainWindow;
using Module = Autofac.Module;

namespace Enrollment.Scanner
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var container = Bootstrap();

            RunApplication(container);
        }

        private static IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<InjectionModule>();

            var container = builder.Build();

            return container;
        }

        private static void RunApplication(IContainer container)
        {
            var app = new App();
            var mainWindow = container.Resolve<MainWindowView>();
            if (mainWindow != null)
            {
                app.Run(mainWindow);
            }
        }
    }

    internal class InjectionModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(executingAssembly)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}