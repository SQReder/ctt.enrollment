using System;
using System.Windows.Forms;
using Autofac;

namespace Enrollment.DesktopScanner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = Bootstrap();

            Run(container);
        }

        private static void Run(IContainer container)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = container.Resolve<MainForm>();

            Application.Run(mainForm);
        }

        private static IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<InjectionModule>();

            return builder.Build();
        }
    }
}
