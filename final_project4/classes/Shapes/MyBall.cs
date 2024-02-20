using System;
using System.Collections.Generic;
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

            return Math.Abs(y - point.Y) < 2 && SettingsClass.isBetween(line.x1, point.X, line.x2);
           
             //debug 
          /*   if (val)
            {
                MyLine myLine = new MyLine(point ,new Point(point.X,y));
               
                if (SettingsClass.GameCanvas !=null)
                {
                    myLine.AddToCanvas( SettingsClass.GameCanvas);
                    line.AddToCanvas(SettingsClass.GameCanvas );
                }
                return true;
            }*/
           // return val;
        }
        public CollisionType CollCheckForPolygon(MyPolygon Polygon2)
        {
            
            if (Polygon2 == null || Polygon2.lines == null || Rect.lines == null ) return CollisionType.False;//if there isn't a polygon there isn't collision

            foreach (MyLine line1 in Rect.lines)
            {
                foreach (MyLine line2 in Polygon2.lines)
                {
                    PointCol point = line1.Collision(line2);
                   
                    if (point.Collision)
                    {
                        line1.UpdateLineCollisionHitArr();
                        line2.UpdateLineCollisionHitArr();
                        line2.print(line2.FpsHitIds);
                        if (line2.SumDifferentId < 10)
                        {
                            Body.y -= Body.vy / SettingsClass.current_FPS*1.5 ;
                            Body.x -= Body.vx / SettingsClass.current_FPS *1.5 ;
                           // return CollisionType.Wall;
                        }
                        return CollisionHandler(Polygon2, line2);
                    }
                }
            }
            return CollisionType.False;

            // crazy one-liner  return (from line1 in lines from line2 in from line2 in Polygon2.lines let point = line1.Collision(line2) where point.Collision select line2 select CollisionHandler(Polygon2, line2)).FirstOrDefault();
        }

        private CollisionType CollisionHandler(MyPolygon Polygon2, MyLine line2)
        {
            switch (line2.LineType)
            {
                case LineType.Win:
                    {
                        Polygon2.realPolygon.Stroke = new SolidColorBrush(Windows.UI.Colors.Green);
                        Polygon2.realPolygon.StrokeThickness = 5;
                        return CollisionType.Win;
                    }
                case LineType.Coin:
                    {
                        return CollisionType.Coin;
                    }
            }

            //by checking the last time the object was hit the object will not "drown " in the line
            CollisionRegularLine(line2);

            return CollisionType.Wall;
        }

        public void CollisionRegularLine(MyLine line2)
        {
            /* if (DebugClass.FrameCounter - FrameHitId ==1)//temporary solution
              {
                  this.Body.y -= Body.vy/SettingsClass.current_FPS*2;
                  this.Body.x -= Body.vx / SettingsClass.current_FPS*2;
              }*/
            

            double ang = GetAngleBetweenVectorAndLine(line2.Degree);

            double friction = line2.Friction;//0.8 is for loss of speed when collision 
            DebugClass.angleCollision = ang;

            double rad = SettingsClass.ConvertAngleRadian(ang);

            double vectorValue = Math.Sqrt(Body.vx * Body.vx + Body.vy * Body.vy) * friction;

            Body.vx = vectorValue * Math.Cos(rad);

            Body.vy = vectorValue * Math.Sin(rad);
        }


    }
}