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
using Windows.UI.Xaml.Shapes;
using Point = Windows.Foundation.Point;

namespace final_project4.classes
{
    //Resizable Polygon
    public class ReSizablePolygon:ReSizable
    {
        public Polygon imgPolygon { get; set; }

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
                imgPolygon.Points= value;
                CreateLineList();
            }
        }

        List<LineCol> lines;

        //public GameCanvas gameCanvas; does'nt need game canvas
        public LineCol speedVector;


        public uint FrameHitId = 0;

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

            imgPolygon = CreateRect(x,y,width,height,angle);
            //_pointsImg = new PointCollection();
            _pointsImg = imgPolygon.Points;
            realPolygon= CreateRect(SettingsClass.Convert_To_Real(x),SettingsClass.Convert_To_Real(y), SettingsClass.Convert_To_Real(width), SettingsClass.Convert_To_Real(height), angle);


            
        }
        public void UpdateRealSize()
        {
          
            for (int i = 0; i < imgPolygon.Points.Count; i++)
            {
                realPolygon.Points[i] = Convert_To_Real_Point(imgPolygon.Points[i].X, imgPolygon.Points[i].Y);
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
                Opacity =0.4,
            };

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

            lines  = new List<LineCol>();// reset the list 


            //this method connects all the point , and leave the last point to connects with first point 
            for (int i = 0; i < _pointsImg.Count- 1; i++)
            {
                lines.Add(new LineCol(_pointsImg[i], _pointsImg[i+1]));
            }
            lines.Add(new LineCol(_pointsImg.Last(), _pointsImg[0])) ;

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


        //don't do inertance for coll maybe check 
           private bool CollCheckForPolygon(ReSizablePolygon Polygon2) // need to check what i can do for 
           {
               if (Polygon2 == null || Polygon2.lines==null||lines==null || DebugClass.FrameCounter -FrameHitId<10) return false;//if there isn't a polygon there isn't collision


               foreach (LineCol line1 in lines)
               {
                   foreach (LineCol line2 in Polygon2.lines )
                   {
                       PointCol point = line1.Collision(line2);
                       if (point.collation)
                        {
                        FrameHitId = DebugClass.FrameCounter;
                        //just for debugging
                        LineCol lineCol = new LineCol(new Point(point.x, point.y), new Point(point.x + 1, point.y));
                        lineCol.AddToCanvas(SettingsClass.GameCanvas);

                        LineCol lineCol4 = this.body.CreateVectorRepresentation();
                        lineCol4.AddToCanvas(SettingsClass.GameCanvas);
                        double a = lineCol4.Degree;// i need to change it to speed line
                        double b = line2.Degree;

                        double ang = (2*b-a);
                        if (body.vx<0)
                        {
                            ang = 180-(2 * b + a);

                        }

                        double vectorValue = Math.Sqrt(body.vx * body.vx + body.vy * body.vy);
                        DebugClass.angleCollision = ang;



                        double rad = SettingsClass.ConvertAngleRadian(ang);


                        body.vx = vectorValue * Math.Cos(rad);

                        body.vy = vectorValue * Math.Sin(rad);

                        // i need to check if the object is hiting somthing again in 10 frame and let it change the speed
                        //again and then ,put 20 fps or more stoper where the body cant have a collistion 




                     /*   if (ang < 0)
                        {
                            body.vy *= -1;
                            body.vx *= -1;
                        }
                     */


                        /* if (body.vy>0 )
                         {
                             body.y += 20;

                         }
                         else
                         {
                             body.y -= 20;

                         }
                         if (body.vx>0)
                         {
                             body.x += 20;
                         }
                         else
                         {
                             body.x -= 20;
                         }
      */




                        return true;
                    }
                }


               }
               return false;
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
