using System;
using System.Threading;
using System.Windows.Forms;

namespace DBConnect
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form mainForm = new MainForm();
            Application.Run(mainForm);

            //Application.ThreadException += new ThreadExceptionEventHandler(ErrorHandlerForm.Form1_UIThreadException);
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }
    }
}
