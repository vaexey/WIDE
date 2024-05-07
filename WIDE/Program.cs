using WIDE.Controller;
using WIDE.View;

namespace WIDE
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.ThreadException += HandleUIException;
            Application.Run(new StartForm());
        }

        private static void HandleUIException(object? sender, ThreadExceptionEventArgs e)
        {
            // TODO: translate
            Messages.Show("Unhandled exception", e.Exception.ToString(), true);
            Application.Exit();
        }
    }
}