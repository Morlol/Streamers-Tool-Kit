using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Streamers_Tool_Kit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Error: " + e + " has occurred", "Error :(");
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            File.WriteAllText(System.IO.Path.Combine("OBS", "RNG_List_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("OBS", "Stream_Message_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("OBS", "RNG_Number_OBS.txt"), "");
        }
    }
}
