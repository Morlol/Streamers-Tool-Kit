using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;

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
            File.WriteAllText(System.IO.Path.Combine("OBS", "Stream_Message1_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("OBS", "Stream_Message2_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("OBS", "Stream_Message3_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("OBS", "Stream_Message4_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("OBS", "Stream_Message5_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("OBS", "Stream_Message6_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("OBS", "RNG_Number_OBS.txt"), "");
            App.Current.Shutdown();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MessageBox.Show("Welcome to Streamers Tool Kit! A program written by @SAFT_Morlol. Please keep in mind that this program is in early access!", "Credits: @SAFT_Morlol");
        }
    }
}
