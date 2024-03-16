using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace final_project4.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataPage2 : Page
    {
        public DataPage2()
        {
            this.InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void PlayersLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();

        }
        public async void UpdateTable()
        {
            ServiceReference1.IService1 s = new ServiceReference1.Service1Client();
            ComboBoxItem comboBoxItem = DataBox.SelectedItem as ComboBoxItem;

            int result = 5;

            switch (comboBoxItem.Content.ToString())
            {
                case "GetLevelStats":

                    LstUsers.ItemsSource = await s.GetLevelStatsAsync();//ng

                    break;
                case "GetLevelStatsPerUser":

                    LstUsers.ItemsSource = await s.GetLevelStatsPerUserAsync((string)MyTextBox.Text);
                    break;
                case "GetLevelStatsByLevelId":

                    if (int.TryParse(MyTextBox.Text, out result))
                    {
                        LstUsers.ItemsSource = await s.GetLevelStatsByLevelIdAsync(result);

                    }


                    break;
                case "GetLevelStatsById":

                    if (int.TryParse(MyTextBox.Text, out result))
                    {
                        LstUsers.ItemsSource = new List<ServiceReference1.LevelStats> { await s.GetLevelStatsByIdAsync(result) };

                    }
                    break;

            }

            //LstUsers.ItemsSource = find;
        }
        private void DataBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem comboBoxItem = DataBox.SelectedItem as ComboBoxItem;
            if (MyTextBox == null) return;
           
            switch (comboBoxItem.Content.ToString())
            {
                case "GetLevelStats":
                    MyTextBox.Visibility = Visibility.Collapsed;
                    break;
                case "GetLevelStatsPerUser":
                    MyTextBox.Visibility = Visibility.Visible;
                    break;
                case "GetLevelStatsByLevelId":
                    MyTextBox.Visibility = Visibility.Visible;
                    break;
                case "GetLevelStatsById":
                    MyTextBox.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
