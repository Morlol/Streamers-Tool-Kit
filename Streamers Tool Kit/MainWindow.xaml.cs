using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Streamers_Tool_Kit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory("OBS");
            Directory.CreateDirectory("SaveFiles");

            // Creating Files for OBS
            File.WriteAllText(System.IO.Path.Combine("OBS", "RNG_List_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("OBS", "Stream_Message_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("OBS", "RNG_Number_OBS.txt"), "");
        }

        // Start of the Random List Code
        // Creates the Variable for the List
        List<string> ListItems = new List<string>();

        private void ListEntry(object sender, KeyEventArgs e)
        {
            // Checks if the Key is enter
            if (e.Key == Key.Enter)
            {
                //checks if it is only spaces or empty
                if (string.IsNullOrWhiteSpace(ListInput.Text))
                {
                    // Nothing should happen when its only spaces or empty
                } 
                else
                {
                    // Adds the Content to the List
                    ListItems.Add(ListInput.Text);

                    // Creates the UI
                    Label label = new Label();
                    label.Content = ListInput.Text;
                    Border b = new Border();
                    StackPanel stackPanel = new StackPanel();
                    b.Child = stackPanel;
                    stackPanel.Children.Add(label);
                    b.Margin = new Thickness(-5);
                    ListContainer.Children.Add(b);

                    // Other Stuff
                    SaveList.IsEnabled = true;
                    DeletMode.IsEnabled = true;
                    ResetList.IsEnabled = true;
                    ChooseRNGList.IsEnabled = true;
                    ListInput.Text = "";
                }
            }
        }

        private void RNGListChoose(object sender, RoutedEventArgs e)
        {
            Random rng = new Random();
            int rngoutput = rng.Next(0, ListItems.Count);
            string stringoutput = ListItems[rngoutput];
            MessageBox.Show("Your Random Item is: " + stringoutput + "!", stringoutput);
            RNGListContent.Content = stringoutput;
            File.WriteAllText(System.IO.Path.Combine("OBS", "RNG_List_OBS.txt"), stringoutput);
        }

        private void LoadList_Click(object sender, RoutedEventArgs e)
        {
            string JsonContent = "";

            // Creates a Open File Dialog
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open a Random List";
            openFile.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory().ToString(), "SaveFiles");
            openFile.Filter = "Random List Files | *.RLF";
            if (openFile.ShowDialog() == true)
               JsonContent = File.ReadAllText(openFile.FileName);

            // Reading the Json File
            dynamic Json = JsonConvert.DeserializeObject(JsonContent);
            foreach (string s in Json)
            {
                ListItems.Add(s);

                // Creates the UI
                Label label = new Label();
                label.Content = s;
                Border b = new Border();
                StackPanel stackPanel = new StackPanel();
                b.Child = stackPanel;
                stackPanel.Children.Add(label);
                b.Margin = new Thickness(-5);
                ListContainer.Children.Add(b);

                // Other Stuff
                SaveList.IsEnabled = true;
                DeletMode.IsEnabled = true;
                ResetList.IsEnabled = true;
                ChooseRNGList.IsEnabled = true;
                ListInput.Text = "";
            }
        }

        private void SaveList_Click(object sender, RoutedEventArgs e)
        {
            // Creates the Json Format
            string json = JsonConvert.SerializeObject(ListItems, Formatting.Indented);

            // Opens a Save File Dialog
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "Save your Random List";
            saveFile.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory().ToString(), "SaveFiles");
            saveFile.Filter = "Random List Files | *.RLF";
            saveFile.DefaultExt = ".RLF";
            saveFile.FileName = "Random List SaveFile";
            if (saveFile.ShowDialog() == true)
                File.WriteAllText(saveFile.FileName, json);
        }

        bool delete = false;
        private void List_RemoveMode(object sender, RoutedEventArgs e)
        {
            if (delete)
            {
                foreach (Border b in ListContainer.Children)
                {
                    StackPanel stackpanel = b.Child as StackPanel;
                    stackpanel.Children.RemoveAt(1);
                }
            }
            else
            {
                foreach (Border b in ListContainer.Children)
                {
                    StackPanel stackPanel = b.Child as StackPanel;
                    Button button = new Button();
                    button.Content = "Delete Item";
                    button.Click += DeleteListItem;
                    stackPanel.Children.Add(button);
                }
            }
        }

        private void DeleteListItem(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            StackPanel stackPanel = button.Parent as StackPanel;
            Border b = stackPanel.Parent as Border;
            int id = ListContainer.Children.IndexOf(b);
            ListContainer.Children.RemoveAt(id);
            ListItems.RemoveAt(id);
        }

        private void ResetList_Click(object sender, RoutedEventArgs e)
        {
            ListItems.Clear();
            ListContainer.Children.Clear();

            SaveList.IsEnabled = false;
            DeletMode.IsEnabled = false;
            ResetList.IsEnabled = false;
            ChooseRNGList.IsEnabled = false;
        }
    }
}
