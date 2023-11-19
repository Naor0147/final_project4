using final_project4.classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace final_project4.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage1 : Page
    {
        int _angle = 0;
        GameCanvas gameCanvas;
        public GamePage1()
        {
            this.InitializeComponent();
            gameCanvas = new GameCanvas(Canvas);

            CompositionTarget.Rendering += CompositionTarget_Rendering;
            CreatePolygon(300,300,200,200,20);
            gameCanvas.UpdateSize(1, 1);
           
        }
        private void CreatePolygon( int x, int y,double height,double width,double angle)
        {
            Polygon myPolygon = new Polygon();
            myPolygon.Stroke = new SolidColorBrush(Windows.UI.Colors.Purple);
            myPolygon.StrokeThickness = 6;
            myPolygon.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
            myPolygon.Fill.Opacity = 0.4;


            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            


            // Define the points of the polygon
            PointCollection polygonPoints = new PointCollection();
            polygonPoints.Add(Convert_To_point(x,y));
            polygonPoints.Add(Convert_To_point((x - height * sin), (y + height * cos)));
            polygonPoints.Add(Convert_To_point((x + width * cos - height * sin), (y + width * sin + height * cos)));
            polygonPoints.Add(Convert_To_point((x + width * cos), (y + width * sin)));

            myPolygon.Points = polygonPoints;
            
            gameCanvas.AddToCanvas(myPolygon);
        }

        public Windows.Foundation.Point Convert_To_point(double x, double y)
        {
            return new Windows.Foundation.Point((int)x, (int)y);
        }



        private void CompositionTarget_Rendering(object sender, object e)
        {
            _angle++;    
          //  CreateRect(300, 300, 200, 200, _angle);
            gameCanvas.MoveAll(1, 1);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SettingsClass.Change_To_Right_Screen_Ratio();
        }
    }
}
