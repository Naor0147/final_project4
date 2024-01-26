using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace final_project4.classes.Shapes.Polygons
{
    public class MyWall : MyPolygon
    {
        public MyWall(double x,double y, double height, double width, double angle = 0, string Id = "") : base(new PhysicBody(x,y), height, width, angle, Id)
        {
            /* Stroke = new SolidColorBrush(Windows.UI.Colors.Purple),
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Windows.UI.Colors.Blue) ,
                Opacity =0.7,*/
            realPolygon.Stroke = new SolidColorBrush(Windows.UI.Colors.Purple);
            realPolygon.StrokeThickness = 2;
            realPolygon.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
            realPolygon.Opacity = 0.7;
            //this.lines[0]; 
        }
    }
}
