using System;
using System.Windows.Forms;
using BaiTap_WinForms.Forms;
using System.Globalization;

namespace BaiTap_WinForms
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
            var vi = new CultureInfo("vi-VN");
            CultureInfo.DefaultThreadCurrentCulture = vi;
            CultureInfo.DefaultThreadCurrentUICulture = vi;
            Thread.CurrentThread.CurrentCulture = vi;       // luồng giao diện hiện tại
            Thread.CurrentThread.CurrentUICulture = vi;

            ApplicationConfiguration.Initialize();

            var loginForm = new FormLogin();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormMain());
            }
        }
    }
}
