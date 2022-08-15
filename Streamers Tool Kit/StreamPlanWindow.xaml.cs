using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Threading;
using System.Threading.Tasks;

namespace Streamers_Tool_Kit
{
    /// <summary>
    /// Interaction logic for StreamPlanWindow.xaml
    /// </summary>
    public partial class StreamPlanWindow : Window
    {
        bool showen = false;

        public StreamPlanWindow()
        {
            InitializeComponent();
        }

        public async void AddStreamWindow(MainWindow.StreamInfo i)
        {
            Border b = new Border();
            b.Opacity = 0;
            b.Margin = new Thickness(5);
            b.Background = new SolidColorBrush(SystemColors.HighlightColor);
            b.CornerRadius = new CornerRadius(6);
            Grid grid = new Grid();
            b.Child = grid;

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();

            Label Titel1 = new Label();
            Titel1.Content = "Titel";
            Titel1.Opacity = 0.65;
            Label Extra1 = new Label();
            Extra1.Content = "Description";
            Extra1.Opacity = 0.65;
            Label Day1 = new Label();
            Day1.Content = "Date";
            Day1.Opacity = 0.65;
            Label Time1 = new Label();
            Time1.Content = "Time";
            Time1.Opacity = 0.65;

            Grid.SetColumn(Titel1, 0);
            Grid.SetColumnSpan(Titel1, 2);
            Grid.SetColumn(Extra1, 3);
            Grid.SetColumnSpan(Extra1, 2);
            Grid.SetColumn(Day1, 6);
            Grid.SetColumn(Time1, 7);

            ColumnDefinition gridCol0 = new ColumnDefinition();
            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();
            ColumnDefinition gridCol3 = new ColumnDefinition();
            ColumnDefinition gridCol4 = new ColumnDefinition();
            ColumnDefinition gridCol5 = new ColumnDefinition();
            ColumnDefinition gridCol6 = new ColumnDefinition();
            ColumnDefinition gridCol7 = new ColumnDefinition();
            ColumnDefinition gridCol8 = new ColumnDefinition();

            grid.ColumnDefinitions.Add(gridCol0);
            grid.ColumnDefinitions.Add(gridCol1);
            grid.ColumnDefinitions.Add(gridCol2);
            grid.ColumnDefinitions.Add(gridCol3);
            grid.ColumnDefinitions.Add(gridCol4);
            grid.ColumnDefinitions.Add(gridCol5);
            grid.ColumnDefinitions.Add(gridCol6);
            grid.ColumnDefinitions.Add(gridCol7);
            grid.ColumnDefinitions.Add(gridCol8);
            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);

            Label Titel = new Label();
            Label Extra = new Label();
            Label Day = new Label();
            Label Time = new Label();

            Titel.Content = i.StreamTitel;
            Extra.Content = i.StreamExtra;
            Day.Content = i.StreamDay;
            Time.Content = i.StreamTime;

            Grid.SetColumn(Titel, 0);
            Grid.SetColumnSpan(Titel, 2);
            Grid.SetColumn(Extra, 3);
            Grid.SetColumnSpan(Extra, 2);
            Grid.SetColumn(Day, 6);
            Grid.SetColumn(Time, 7);

            Grid.SetRow(Titel, 1);
            Grid.SetRow(Extra, 1);
            Grid.SetRow(Day, 1);
            Grid.SetRow(Time, 1);

            grid.Children.Add(Titel);
            grid.Children.Add(Extra);
            grid.Children.Add(Day);
            grid.Children.Add(Time);
            grid.Children.Add(Titel1);
            grid.Children.Add(Extra1);
            grid.Children.Add(Day1);
            grid.Children.Add(Time1);
            grid.Margin = new Thickness(10);

            StreamsContainer.Children.Add(b);

            if (showen)
            {
                DoubleAnimation opacityAnimation2 = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.7)));
                b.BeginAnimation(OpacityProperty, opacityAnimation2);
                await Task.Delay(500);
            }
        }

        public void Showing()
        {
            
            showen = true;
            Animation();
        }

        public async void Animation()
        {
            await Task.Delay(1000);
            foreach (Border b in StreamsContainer.Children)
            {
                  DoubleAnimation opacityAnimation = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.7)));
                  b.BeginAnimation(OpacityProperty, opacityAnimation);
                  await Task.Delay(500);
            }
        }
    }
}
