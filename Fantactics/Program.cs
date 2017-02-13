using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flame.Debug;

namespace Fantactics
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ApplicationExit += Application_ApplicationExit;
            Fantactics f = new Fantactics();
            f.Run(30);

        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            DebugConsole.MirrorToFile();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            DebugConsole.Output("=======CRITICAL ERROR=======");
            DebugConsole.Output(ex.Message);
            DebugConsole.Output("\n\r" + ex.StackTrace);
            DebugConsole.Output("============================");
            DebugConsole.MirrorToFile();
        }
    }
}
