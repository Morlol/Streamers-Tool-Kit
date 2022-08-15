using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

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
            Directory.CreateDirectory("_OBS");
            Directory.CreateDirectory("_SaveFiles");

            // Creating Files for OBS
            File.WriteAllText(System.IO.Path.Combine("_OBS", "RNG_List_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message1_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message2_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message3_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message4_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message5_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message6_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "RNG_Number_OBS.txt"), "");
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
                    b.Margin = new Thickness(-1);
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
            File.WriteAllText(System.IO.Path.Combine("_OBS", "RNG_List_OBS.txt"), stringoutput);
        }

        private void LoadList_Click(object sender, RoutedEventArgs e)
        {
            string JsonContent = "";

            // Creates a Open File Dialog
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open a Random List";
            openFile.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory().ToString(), "_SaveFiles");
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
                b.Margin = new Thickness(10);
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
            saveFile.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory().ToString(), "_SaveFiles");
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

        // Start of the Streamplan code!
        // Making the Info Class
        public class StreamInfo
        {
            public string StreamTitel;
            public string StreamExtra;
            public string StreamDay;
            public string StreamTime;
        }

        List<StreamInfo> Streams = new List<StreamInfo>();
        public StreamPlanWindow StreamsWindow;


        private void AddStream(object sender, RoutedEventArgs e)
        {
            if (StreamTitelInput.Text != string.Empty && StreamExtralInput.Text != string.Empty && StreamDayInput.Text != string.Empty && StreamTimelInput.Text != string.Empty)
            {
                StreamInfo stream = new StreamInfo
                {
                    StreamTitel = StreamTitelInput.Text,
                    StreamExtra = StreamExtralInput.Text,
                    StreamDay = StreamDayInput.Text,
                    StreamTime = StreamTimelInput.Text
                };

                if (StreamsWindow != null)
                    StreamsWindow.AddStreamWindow(stream);

                Streams.Add(stream);
                MessageBox.Show("New Stream added!", stream.StreamTitel);

                StreamTitelInput.Text = "";
                StreamExtralInput.Text = "";
                StreamDayInput.Text = "";
                StreamTimelInput.Text = "";
            }
            else
            {
                MessageBox.Show("You have to fill in all details", "Missing Information");
            }
        }

        private void ResetStreams(object sender, RoutedEventArgs e)
        {
            Streams.Clear();
            StreamsWindow.StreamsContainer.Children.Clear();
        }

        private void OpenStreamPlan(object sender, RoutedEventArgs e)
        {
            StreamsWindow = new StreamPlanWindow();
            foreach (StreamInfo i in Streams)
            StreamsWindow.AddStreamWindow(i);
            StreamsWindow.Show();
            StreamsWindow.Showing();
        }

        private void SaveStreamPlan(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(Streams, Formatting.Indented);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save your Streamplan";
            saveFileDialog.Filter = "Streamplan Files |*.SPF";
            saveFileDialog.DefaultExt = ".SPF";
            saveFileDialog.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory().ToString(), "_SaveFiles");
            saveFileDialog.FileName = "Streamplan Savefile";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, json);
        }

        private void LoadStreamPlan(object sender, RoutedEventArgs e)
        {
            string fileinfo = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Load your Streamplan";
            openFileDialog.Filter = "Streamplan Files |*.SPF";
            openFileDialog.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory().ToString(), "_SaveFiles");
            if (openFileDialog.ShowDialog() == true)
                fileinfo = File.ReadAllText(openFileDialog.FileName);
            dynamic json = JsonConvert.DeserializeObject<List<StreamInfo>>(fileinfo);

            foreach (StreamInfo i in json)
            {
                Streams.Add(i);
                if(StreamsWindow != null)
                    StreamsWindow.AddStreamWindow(i);
            }

            MessageBox.Show("Successfully loaded in the Streamplan!", "Streamplan ready");
        }

        // Start of the On Stream Message Code
        private void MessageEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox message = sender as TextBox;
                if (message == Message1)
                {
                    File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message1_OBS.txt"), message.Text);
                }
                else if (message == Message2)
                {
                    File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message2_OBS.txt"), message.Text);
                }

                else if (message == Message3)
                {
                    File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message3_OBS.txt"), message.Text);
                }
                else if (message == Message4)
                {
                    File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message4_OBS.txt"), message.Text);
                }
                else if (message == Message5)
                {
                    File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message5_OBS.txt"), message.Text);
                }
                else if (message == Message6)
                {
                    File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message6_OBS.txt"), message.Text);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(StreamsWindow != null)
            StreamsWindow.Close();

            File.WriteAllText(System.IO.Path.Combine("_OBS", "RNG_List_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message1_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message2_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message3_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message4_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message5_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "Stream_Message6_OBS.txt"), "");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "RNG_Number_OBS.txt"), "");
        }

        // RNG Number Code:

        private void ChooseRNG(object sender, RoutedEventArgs e)
        {
            int min = ((int)RNG_NumMin.Value.Value);
            int max = ((int)RNG_NumMax.Value.Value);
            Random rng = new Random();

            int number = rng.Next(min, max);
            RNG_Result.Content = number;
            MessageBox.Show("Your result is: " + number, "Random Number Generator Result");
            File.WriteAllText(System.IO.Path.Combine("_OBS", "RNG_Number_OBS.txt"), number.ToString());
        }
    }
}
