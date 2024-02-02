using NetTopologySuite.Operation.Overlay.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace final_project4.classes.Shapes.Polygons
{
    public enum WallStyle
    {
        Regular, SideWall, Floor, Celling
    }
    public class MyWall : MyPolygon
    {
        private WallStyle Style;
        public MyWall(PhysicBody physicBody, double height, double width,WallStyle wallStyle  ,double angle = 0, string Id = "") : base(physicBody, height, width, angle, Id)
        {
            
            this.Style = wallStyle;
            ChangeAppearance();
            
        }

        public void ChangeAppearance()
        {
            //realPolygon.Stroke = new SolidColorBrush(Windows.UI.Colors.Purple);
            //realPolygon.StrokeThickness = 2;
            realPolygon.Opacity = 1;
            switch (Style)
            {
                case WallStyle.Regular:

                   
                     realPolygon.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
                     realPolygon.Opacity = 0.7;
                    break;
                case WallStyle.SideWall:

                    
                    realPolygon.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("ms-appx:///Images/BrickWall2.jpg")),
                        Stretch = Stretch.Fill
                    };
                        
                    break;

                case WallStyle.Floor:

                   
                    realPolygon.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("ms-appx:///Images/floor.png")),
                        Stretch = Stretch.Fill
                    };
                    break;
                case WallStyle.Celling:
                    realPolygon.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("ms-appx:///Images/celling.jpg")),
                        Stretch = Stretch.Fill
                    };
                    break;
            }
        }


        public override CollisionType CollCheck(ReSizable reSizable)
        {

            return base.CollCheck(reSizable);
        }
    }
}
