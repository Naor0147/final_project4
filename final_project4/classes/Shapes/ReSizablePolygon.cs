using final_project4.classes.Shapes;
using NetTopologySuite.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Threading;
using Windows.Foundation;
using Windows.UI.ViewManagement.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Point = Windows.Foundation.Point;

namespace final_project4.classes
{
    //Resizable Polygon
    public class ReSizablePolygon:ReSizable
    {
      

        public Polygon realPolygon{ get; set; }

        public double  angle;
        

        public string Id;

        private PointCollection __imgPoints;

        private PointCollection _pointsImg
        {
            get
            {
                return __imgPoints;
            }

            set
            {
                __imgPoints = value;
         
                CreateLineList();
            }
        }

        List<MyLine> lines;

        //public GameCanvas gameCanvas; does'nt need game canvas
        public MyLine speedVector;


        public uint FrameHitId = 0;



        //parameters that could be changed 

        public double Opacity 
        {
            get { return realPolygon.Opacity; }
            set { 
                realPolygon.Opacity = value;
            }
        }

       
        public Brush TheBrush
        {
            get
            {
                return realPolygon.Stroke;
                
            }
            set
            {
                realPolygon.Stroke = value;
            }
        }










        public ReSizablePolygon(PhysicBody physicBody,double height ,double width ,double angle=0,string Id=""):base(physicBody,height,width)
        {
            //physics
            this.body = physicBody;
            //size and angle
            this.height = height;
            this.width = width;
            this.angle = angle;

            //for better readability 
            double x = physicBody.x;
            double y = physicBody.y;

            this.Id = Id;

            _pointsImg = CreateRect(x, y, width, height, angle).Points;
            realPolygon= CreateRect(SettingsClass.Convert_To_Real(x),SettingsClass.Convert_To_Real(y), SettingsClass.Convert_To_Real(width), SettingsClass.Convert_To_Real(height), angle);


            
        }
        public void UpdateRealSize()
        {
          
            for (int i = 0; i < _pointsImg.Count; i++)
            {
                realPolygon.Points[i] = Convert_To_Real_Point(_pointsImg[i].X, _pointsImg[i].Y);
            }
            
        }
        


        public override void UpdatePosAndSize()
        {
            _pointsImg= RectPoints(body.x, body.y, width, height, angle);
            
            UpdateRealSize();
        }


        
        
      


        private Polygon CreateRect(double x, double y, double height, double width, double angle)
        {
            Polygon myPolygon = PolygonAppearance();

            // Define the points of the polygon
            myPolygon.Points = RectPoints(x, y, height, width, angle);
            
            return myPolygon;

        }

        private static Polygon PolygonAppearance()
        {
            return new Polygon()
            {
                Stroke = new SolidColorBrush(Windows.UI.Colors.Purple),
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Windows.UI.Colors.Blue) ,
                Opacity =0.7,
            };
            /*Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/LargeTile.scale-400.png")),
                    Stretch = Stretch.Fill
                },*/

        }

        private PointCollection RectPoints(double x, double y, double height, double width,double angle)
        {

            /// if i put the angle to a really small number close to 0 , collition will work properly , need to if there is posiible way to fix 
            double radian = SettingsClass.ConvertAngleRadian(angle);//convert the angle to radian 
            double sin = Math.Sin(radian);//the sin , cos works in radian so i convert the angle to radian
            double cos = Math.Cos(radian);

            PointCollection polygonPoints = new PointCollection();
            polygonPoints.Add(ConvertToPoint(x, y));
            polygonPoints.Add(ConvertToPoint((x - height * sin), (y + height * cos)));
            polygonPoints.Add(ConvertToPoint((x + width * cos - height * sin), (y + width * sin + height * cos)));
            polygonPoints.Add(ConvertToPoint((x + width * cos), (y + width * sin)));
            return polygonPoints;
        }

     

        public  void CreateLineList()
        {
      
            
            if (_pointsImg == null) return;//if there isn't a list of points there nothing to draw 

            lines  = new List<MyLine>();// reset the list 


            //this method connects all the point , and leave the last point to connects with first point 
            for (int i = 0; i < _pointsImg.Count- 1; i++)
            {
                lines.Add(new MyLine(_pointsImg[i], _pointsImg[i+1]));
            }
            lines.Add(new MyLine(_pointsImg.Last(), _pointsImg[0])) ;

        }



        public override bool CollCheck(ReSizable reSizable)
        {

            switch (reSizable)
            {
                case ReSizablePolygon re:
                    ReSizablePolygon polygon = (ReSizablePolygon)reSizable;
                    return CollCheckForPolygon(polygon);

                case ReSizableBall ba:
                    ReSizableBall reSizableBall = (ReSizableBall)reSizable;
                    return CollCheckForBall(reSizableBall);

            }

            
            return false;   
        }


         public void ChangeAperecnce(SolidColorBrush solidColorBrush)
            {
            if (solidColorBrush == null) return;
            realPolygon.Fill = solidColorBrush;
          } 

        //this change how the block looks
        public void ChangeAperecnce(string FileName)
        {
            realPolygon.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Images/"+FileName)),
                Stretch = Stretch.Fill
            };
        }



           private bool CollCheckForPolygon(ReSizablePolygon Polygon2) 
           {
               if (Polygon2 == null || Polygon2.lines==null||lines==null || DebugClass.FrameCounter -FrameHitId<5) return false;//if there isn't a polygon there isn't collision


               foreach (MyLine line1 in lines)
               {
                   foreach (MyLine line2 in Polygon2.lines )
                   {
                       PointCol point = line1.Collision(line2);
                       if (point.collation)
                    {

                        //by checking the last time the object was hit the object will not "drown " in the line 
                        FrameHitId = DebugClass.FrameCounter;
                        /*if (IsGround(line2))
                        {
                            return true;
                        }*/


            //just for debugging
            //MyLine lineCol = new MyLine(new Point(point.x, point.y), new Point(point.x + 1, point.y));
            //lineCol.AddToCanvas(SettingsClass.GameCanvas);

                        double ang = GetVectorFutreAngle(line2.Degree);


                        double Friction = 0.8;//0.8 is for loss of speed when coliision
                        double vectorValue = Math.Sqrt(body.vx * body.vx + body.vy * body.vy) *Friction;
                        DebugClass.angleCollision = ang;



                        double rad = SettingsClass.ConvertAngleRadian(ang);


                        body.vx = vectorValue * Math.Cos(rad);

                        body.vy = vectorValue * Math.Sin(rad);




                        return true;
                    }
                }


               }
               return false;
           }

        private bool IsGround(MyLine line)
        {
            if (body.vy<5 )
            {
                body.HaveGravity = false;
                if (line.m == 0)
                {
                    body.ay = 0;
                    body.vy = 0;
                }
                else 
                {
                    body.ay=98*Math.Cos(line.Radian);
                    body.ax =- 98 * Math.Sin(line.Radian);

                }

                return true;
            }
            return false;
        }


        private double GetVectorFutreAngle(double Degree)
        {
            MyLine lineCol4 = this.body.CreateVectorRepresentation();
            //lineCol4.AddToCanvas(SettingsClass.GameCanvas);
            double a = lineCol4.Degree;
            double b = Degree;//line2.Degree

            double ang = (2 * b - a);
            if (body.vx < 0)
            {
                ang = 180 - (2 * b + a);

            }

            return ang;
        }



        private bool CollCheckForBall(ReSizableBall ball)
       {
            return CollCheckForPolygon(ball.rect);
       }


        public override void AddToCanvas(GameCanvas gameCanvas)
        {
            gameCanvas.AddToCanvas(this);
        }


        public Point ConvertToPoint(double x, double y)=> new Point(x, y);
        public Point Convert_To_Real_Point(double x, double y) => new Point((SettingsClass.Convert_To_Real(x)),SettingsClass.Convert_To_Real(y));

       
    }
}
