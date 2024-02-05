using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace final_project4.classes.Shapes
{
    public class MyBall : ReSizable
    {
        public Ellipse BallEllipse;

        public double Size;
        public MyPolygon Rect;

        public MyBall(PhysicBody body, double size) : base(body, size, size)
        {
            this.Size = size;

            Rect = new MyPolygon(body, size, size);

            this.BallEllipse = CreateEllipse(body.xReal, body.yReal, SettingsClass.Convert_To_Real(size), Colors.Red);
        }

        public Ellipse CreateEllipse(double x, double y, double size, Color color)
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = new SolidColorBrush(color),// i can also use imageBrush for photos
                StrokeThickness = 2,
                Width = size,
                Height = size,
            };

            /*
            Ellipse ellipse = new Ellipse
            {
                Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/LargeTile.scale-400.png")),
                    Stretch = Stretch.Fill
                },
                StrokeThickness = 2,
                Width = size,
                Height = size,
            };*/

            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            return ellipse;
        }

        public override void UpdatePosAndSize()
        {
            Canvas.SetLeft(BallEllipse, Body.xReal);
            Canvas.SetTop(BallEllipse, Body.yReal);
            Rect.UpdatePosAndSize();
            BallEllipse.Width = SettingsClass.Convert_To_Real(Size);
            BallEllipse.Height = SettingsClass.Convert_To_Real(Size);
        }

        public override string ToString()
        {
            return $"physic[{Body}] ball size:{Size}";
        }

        public override CollisionType CollCheck(ReSizable reSizable)
        {
            return Rect.CollCheck(reSizable);
        }

        public override void AddToCanvas(GameCanvas gameCanvas)
        {
            gameCanvas.AddToCanvas(this);
        }

        public double ClickOnTheScreen(Point point)
        {
            Point ballPoint = new Point(Body.x + Width / 2, Body.y + Height / 2);
            //double dis = SettingsClass.DistanceBetweenTwoPoints(ballPoint, point);
            MyLine speedLine = new MyLine(ballPoint, point);
            double rad = speedLine.Radian;
            double angle = speedLine.Degree;

            /*  if (rad > Math.PI/2)
              {
                  rad += Math.PI ;
              }*/
            double VectorSize = 2 * speedLine.VectorMagnitude;

            Body.vx = VectorSize * Math.Cos(rad);
            Body.vy = VectorSize * Math.Sin(rad);
            if (ballPoint.X > point.X)
            {
                Body.vx *= -1;
                Body.vy *= -1;
            }

            return angle;
        }


        public bool OnGround(MyPolygon pol)
        {
            if (pol == null) return false;
            foreach (MyLine myLine in pol.lines )
            {
                if (OnGroundLineCheck(myLine))
                {
                    return true;
                }
            }
            return false;
        }

        public bool OnGroundLineCheck(MyLine line)
        {
            if (Body == null) { return false ; }
            Point point = new Point(Body.x+Width/2, Body.y+Height);// the center bottom of the ball
            double y = line.Get_Y_Value_On_X(point.X);

            bool val = Math.Abs(y - point.Y) < 25;
            if (val)
            {
                return true;
            }
            return Math.Abs(y - point.Y) < 3;
        }
    }
}