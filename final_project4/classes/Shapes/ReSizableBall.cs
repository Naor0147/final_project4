using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace final_project4.classes.Shapes
{
    public class ReSizableBall:ReSizable
    {
        
        public Ellipse ImgEllipse;
        public Ellipse RealEllipse;

        public double size;
        public ReSizablePolygon rect;


        public ReSizableBall(PhysicBody body, double size,GameCanvas gameCanvas):base(body,size,size,gameCanvas)
        {
            this.size = size;

            rect = new ReSizablePolygon(body,size,size,gameCanvas);
            
           // this.ImgEllipse = CreateEllipse(body.x,body.y,size,Colors.Red);
            this.RealEllipse = CreateEllipse(SettingsClass.Convert_To_Real(body.x), SettingsClass.Convert_To_Real(body.y), SettingsClass.Convert_To_Real(size), Colors.Red);

        }

        public Ellipse CreateEllipse(double x, double y ,double size, Color color)
        {
            Ellipse ellipse= new Ellipse
            {
                Fill = new SolidColorBrush(color),// i can also use imageBrush for photos 
                StrokeThickness = 2,
                Width = size,
                Height = size,
            };
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            return ellipse;

        }

        public override void UpdatePosAndSize()
        {
            Canvas.SetLeft(RealEllipse, body.x);
            Canvas.SetTop(RealEllipse, body.y);
            rect.UpdatePosAndSize();
            RealEllipse.Width = SettingsClass.Convert_To_Real(size);
            RealEllipse.Height = SettingsClass.Convert_To_Real(size);
        }



        public override string ToString()
        {
            return $"physic[{body.x}] ball size:{size}";
        }

        public override bool CollCheck(ReSizable reSizable)
        {
            return rect.CollCheck(reSizable);
        }


    }
}
