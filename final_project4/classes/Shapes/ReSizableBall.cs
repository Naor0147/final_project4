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
        public Ellipse realEllipse;

        public double size;
        public ReSizablePolygon rect;


        public ReSizableBall(PhysicBody body, double size):base(body,size,size)
        {
            this.size = size;

           rect = new ReSizablePolygon(body,size,size);
            
           // this.ImgEllipse = CreateEllipse(body.x,body.y,size,Colors.Red);
            this.realEllipse = CreateEllipse(body.xReal,body.yReal, SettingsClass.Convert_To_Real(size), Colors.Red);

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
            Canvas.SetLeft(realEllipse, body.xReal);
            Canvas.SetTop(realEllipse, body.yReal);
            rect.UpdatePosAndSize();
            realEllipse.Width = SettingsClass.Convert_To_Real(size);
            realEllipse.Height = SettingsClass.Convert_To_Real(size);
        }



        public override string ToString()
        {
            return $"physic[{body}] ball size:{size}";
        }

        public override bool CollCheck(ReSizable reSizable)
        {
            return rect.CollCheck(reSizable);
        }
        public override void AddToCanvas(GameCanvas gameCanvas)
        {
            gameCanvas.AddToCanvas(this);
        }


    }
}
