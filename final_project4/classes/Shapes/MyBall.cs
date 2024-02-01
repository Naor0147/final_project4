﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace final_project4.classes.Shapes
{
    public class MyBall:ReSizable
    {
       
        public Ellipse BallEllipse;

        public double size;
        public MyPolygon rect;


        public MyBall(PhysicBody body, double size):base(body,size,size)
        {
            this.size = size;

           rect = new MyPolygon(body,size,size);
            
         
            this.BallEllipse = CreateEllipse(body.xReal,body.yReal, SettingsClass.Convert_To_Real(size), Colors.Red);

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
            Canvas.SetLeft(BallEllipse, body.xReal);
            Canvas.SetTop(BallEllipse, body.yReal);
            rect.UpdatePosAndSize();
            BallEllipse.Width = SettingsClass.Convert_To_Real(size);
            BallEllipse.Height = SettingsClass.Convert_To_Real(size);
        }



        public override string ToString()
        {
            return $"physic[{body}] ball size:{size}";
        }

        public override CollisionType CollCheck(ReSizable reSizable)
        {
            return rect.CollCheck(reSizable);
        }
        public override void AddToCanvas(GameCanvas gameCanvas)
        {
            gameCanvas.AddToCanvas(this);
        }

        public double ClickOnTheScreen(Point point)
        {
            Point ballPoint = new Point(body.x + width/2, body.y+ height/2);
            //double dis = SettingsClass.DistanceBetweenTwoPoints(ballPoint, point);
            MyLine speedLine = new MyLine(ballPoint, point);
            double rad = speedLine.Radian;
            double angle =speedLine.Degree;
           
          /*  if (rad > Math.PI/2)
            {
                rad += Math.PI ;
            }*/
           
            body.vx = speedLine.VectorMagnitude * Math.Cos(rad);
            body.vy = speedLine.VectorMagnitude * Math.Sin(rad);
            if (ballPoint.X>point.X)
            {
                body.vx *= -1;
                body.vy *= -1;
            }

            return angle;
        }



    }
}