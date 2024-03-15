using final_project4.classes.Shapes.Polygons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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


        public void MoveThis()
        {
            MyLine line = new MyLine(Body.x,Body.y,Body.x+Body.vx,Body.vy);
            foreach (MyPolygon item in SettingsClass.GameCanvas.ReList.Where(item => item != null && (item is MyPolygon)))
            {
                if (!(item is MyCoin))
                {
                    foreach(MyLine myLine in item.lines)
                    {

                        PointCol pointCol = line.Collision(myLine);
                        if (pointCol.Collision)
                        {
                            Body.y = pointCol.y;
                            Body.x = pointCol.x;
                            return ;
                        }
                    }
                    
                }
            }
                Body.y += Body.vy;
            Body.x += Body.vx;
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

        public CollisionType NewCollision(MyPolygon myPolygon)
        {
            if (myPolygon == null || myPolygon.lines == null )return CollisionType.False;
            double radius = Size / 2;
            double x = Body.x + radius;
            double y = Body.y + radius;// the center of the ball

            // is there collision 
            bool check = false;
            //what line to check
            MyLine closest = myPolygon.lines[0];//give it a random value , so it will not give error 
            double minDis = radius;
            foreach (MyLine myLine in myPolygon.lines)
            {
                double dis = myLine.GetDisFromPoint(x, y);
                if (dis < radius)
                {
                    switch (myLine.LineType)
                    {
                        case LineType.Win:
                            {
                                myPolygon.realPolygon.Stroke = new SolidColorBrush(Windows.UI.Colors.Green);
                                myPolygon.realPolygon.StrokeThickness = 5;
                                return CollisionType.Win;
                            }
                        case LineType.Coin:
                            {
                                return CollisionType.Coin;
                            }
                    }
                    if (minDis>dis)
                    {
                        minDis = dis;
                        closest = myLine;
                        check = true;
                    }

                }
            }

            if (check)
            {
                SettingsClass.QueueInArr(closest.FpsHitIds, (int)DebugClass.FrameCounter);
                Point p = closest.TheClosestPointOnVector(x, y);
                MyLine myLine1 = new MyLine(p, new Point(x, y));
                //   myLine1.CreateLineVisualization();
                // myLine1.AddToCanvas(SettingsClass.GameCanvas);

                //Debug.WriteLine("" + dis);
                Debug.WriteLine("m: " + closest.m + " angle " + SettingsClass.ConvertRadianDegree(closest.Radian));
                BallNewPostion(p.X, p.Y, minDis, myLine1,closest);
               
            }

            return CollisionType.False;
        }
        public void BallNewPostion(double x, double y, double dis, MyLine line, MyLine interction)
        {

            ChangeSpeed(line);



            //check if the ball is couple of frames in the line 
            if (interction.FpsHitIds[line.FpsHitIds.Length - 1] - interction.FpsHitIds[line.FpsHitIds.Length - 2] > 10)
            {
                return;
            }
            double radius = Size / 2;
            double xCenter = Body.x + radius;
            double yCenter = Body.y + radius;
            double a = line.Radian;
            // double d = Math.Sqrt((radius - dis) / 1 + Math.Pow(Math.Tan(a), 2));
            double d = radius - dis;
            // d = Math.Min(d, Size / 2);
            if (interction.m > 0)
            {
                d = 0 - d;
            }

            line.CreateLineVisualization();
            line.AddToCanvas(SettingsClass.GameCanvas);


            //  // Debug.WriteLine(line.GetDisFromPoint(Body.x + d, Body.y + d * Math.Tan(a)) + "  m: " + interction.m + " angle" + SettingsClass.ConvertRadianDegree(interction.Radian));
            if (interction.m == 0)
            {
                if (y < yCenter) {  
                Body.y += radius;
                    return;
                }
                Body.y -= radius;
                return;
            }
            if (Math.Abs(interction.m) > 1000)
            {
                if (xCenter < x)
                {
                    Body.x -= radius;
                }
                else
                {
                    Body.x += radius;
                }
                return;
            }

           
            Body.x += d * Math.Cos(a);

           
            if (yCenter > y)
            {
                Body.y += d * Math.Sin(a);
                return;
            }
            Body.y -= d * Math.Sin(a);
        }
        public void ChangeSpeed( MyLine myLine)
        {
            if (myLine == null) return;

           
            double ballVx = Body.vx;
            double ballVy = Body.vy;
            double wallAngleRad = myLine.Radian;
            (double rotatedVx, double rotatedVy) = RotateVector(ballVx, ballVy, wallAngleRad);

            // Flip the x-component
            rotatedVx = -rotatedVx;

            // Rotate the resulting vector back
            double finalVx = (rotatedVx * Math.Cos(-wallAngleRad) - rotatedVy * Math.Sin(-wallAngleRad));
            double finalVy = rotatedVx * Math.Sin(-wallAngleRad) + rotatedVy * Math.Cos(-wallAngleRad);

            double angle = SettingsClass.ConvertRadianDegree(wallAngleRad);
            if (0<Math.Abs(angle) && Math.Abs(angle)<87)
            {
                finalVx *=-1;
            }

            Body.vx = SettingsClass.ValueInBorder(-1000,finalVx,1000)*0.8; 
            Body.vy = SettingsClass.ValueInBorder(-1000, finalVy, 1000) * 0.8;
        }
        static (double, double) RotateVector(double vx, double vy, double angle)
        {
            double rotatedVx = vx * Math.Cos(angle) - vy * Math.Sin(angle);
            double rotatedVy = vx * Math.Sin(angle) + vy * Math.Cos(angle);
            return (rotatedVx, rotatedVy);
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
            
            if (Body == null|| line.LineType == LineType.Win|| line.LineType == LineType.Coin) { return false ; }
            Point point = new Point(Body.x+Width/2, Body.y+Height);// the center bottom of the ball
            double y = line.Get_Y_Value_On_X(point.X);


            double dis = y - point.Y;
            if (dis>-5&&dis<0)
            {
                Body.y--;
            }



            return Math.Abs(y - point.Y) < 4&& SettingsClass.isBetween(line.x1, point.X, line.x2);
           
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
                    //IsTheBallGoingsHitTheWall(line2);
                    if (point.Collision)
                    {
                        line1.UpdateLineCollisionHitArr();
                        line2.UpdateLineCollisionHitArr();
                        line2.print(line2.FpsHitIds);

                     /*   if (line2.SumDifferentId < 10 && Math.Abs( line2.m)<100)
                        {
                            /*
                            Body.y -= Body.vy / SettingsClass.current_FPS*1.5 ;
                            Body.x -= Body.vx / SettingsClass.current_FPS *1.5 ;
                            */
                          /*  if (line2.m==0)
                            {
                                //SettingsClass.QueueInArr(Body.OnGround, true);
                                Body.HaveNoSpeed();
                                return CollisionType.Wall;

                            }
                          
*//*
                            double sin = Math.Sin(line2.Radian);
                            double cos = Math.Cos(line2.Radian);
                            double value =  sin * 980;
                            Body.vy = value*sin*0.1;
                            Body.vx = value*cos;
                            Body.y += Body.vy/SettingsClass.current_FPS;
                            return CollisionType.Wall;

                        }*/
                       
                        return CollisionHandler(Polygon2, line2);
                    }
                   
                }
            }
            return CollisionType.False;

            // crazy one-liner  return (from line1 in lines from line2 in from line2 in Polygon2.lines let point = line1.Collision(line2) where point.Collision select line2 select CollisionHandler(Polygon2, line2)).FirstOrDefault();
        }
        public bool IsTheBallGoingsHitTheWall(MyLine line)
        {
            double X = Body.x+Width/2;
            double Y = Body.y+ Height/2;
            double FutureX = Body.x+(Body.vx / SettingsClass.current_FPS * 4);
            double FutureY = Body.y+ (Body.vy / SettingsClass.current_FPS*4);
            MyLine my = new MyLine(X,Y,FutureX, FutureY);

            PointCol value = my.Collision(line);
            if (value.Collision)
            {
                my.CreateLineVisualization();
                SettingsClass.GameCanvas.AddToCanvas(my);
                Body.x = value.x - Body.vx / SettingsClass.current_FPS ; Body.y= value.y - Body.vy / SettingsClass.current_FPS * 4;
            }
            
            return value.Collision;

            
            
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