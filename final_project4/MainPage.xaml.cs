using final_project4.pages;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace final_project4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Polygon myPolygon = new Polygon();
            myPolygon.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
            myPolygon.Points = new PointCollection()
            {
            new Point(500.5, 440.5),
            new Point(20.5, 15.5),
            new Point(300.5, 25.5)
            };
            // Optionally, you can set other properties such as Stroke and StrokeThickness
            myPolygon.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            myPolygon.StrokeThickness = 2;

            // Add the polygon to your UI element (e.g., a Canvas)
            GameCanvas.Children.Add(myPolygon);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage2));

            Button button = sender as Button;
            switch (button.Content.ToString())
            {
                case "page 1":
                    Frame.Navigate(typeof(GamePage1));
                    break;

                case "page 2":
                    Frame.Navigate(typeof(GamePage2));
                    break;
            }
        }
    }
}