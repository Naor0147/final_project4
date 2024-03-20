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
                    whatTableToShow(1);


                    LstUsers.ItemsSource = await s.GetLevelStatsAsync();//ng

                    break;
                case "GetLevelStatsPerUser":
                    whatTableToShow(1);


                    LstUsers.ItemsSource = await s.GetLevelStatsPerUserAsync((string)MyTextBox.Text);
                    break;
                case "GetLevelStatsByLevelId":
                    whatTableToShow(1);

                    if (int.TryParse(MyTextBox.Text, out result))
                    {
                        LstUsers.ItemsSource = await s.GetLevelStatsByLevelIdAsync(result);

                    }


                    break;
                case "GetLevelStatsById":
                    whatTableToShow(1);

                    if (int.TryParse(MyTextBox.Text, out result))
                    {
                        LstUsers.ItemsSource = new List<ServiceReference1.LevelStats> { await s.GetLevelStatsByIdAsync(result) };

                    }
                    break;

                case "GetUsers":
                    LstUsers2.ItemsSource = await s.GetUsersAsync();
                    whatTableToShow(2);


                    break;

                case "FindUser":
                    LstUsers2.ItemsSource = new List<ServiceReference1.User> { await s.FindUserAsync(MyTextBox.Text) };

                    whatTableToShow(2);

                    break;
                case "GetAvgScore":
                    LstUsers3.ItemsSource = await s.GetAvgScoreAsync() ;
                    
                    whatTableToShow(3);
                    break;

            }

            //LstUsers.ItemsSource = find;
        }

        private void whatTableToShow(int table)
        {

            LstUsers.Visibility = Visibility.Collapsed;
            LstUsers2.Visibility = Visibility.Collapsed;
            LstUsers3.Visibility = Visibility.Collapsed;
            switch (table)
            {
                case 1:
                    LstUsers.Visibility = Visibility.Visible;
                    break;
                case 2:
                    LstUsers2.Visibility = Visibility.Visible;
                    break;
                case 3:
                    LstUsers3.Visibility = Visibility.Visible;
                    break;

            }
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
                case "GetUsers":
                    MyTextBox.Visibility = Visibility.Collapsed;
                    break;
                case "FindUser":
                    MyTextBox.Visibility = Visibility.Visible;
                    break;
                case "GetAvgScore":
                    MyTextBox.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
