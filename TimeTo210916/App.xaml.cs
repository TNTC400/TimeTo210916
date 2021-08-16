using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TimeTo210916
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AddRegisterKey();
        }

        private void AddRegisterKey()
        {
            var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);

            string myPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            key.SetValue("TimeTo210916", myPath);
        }

        private void RemoveRegisterKey()
        {
            var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);

            key.DeleteValue("TimeTo210916", false);
        }

        private void Application_Startup_1(object sender, StartupEventArgs e)
        {

        }
    }
}
