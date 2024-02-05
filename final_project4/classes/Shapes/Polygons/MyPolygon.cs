using final_project4.classes.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Point = Windows.Foundation.Point;

namespace final_project4.classes
{
    //Resizable Polygon
    public class MyPolygon : ReSizable
    {
        public Polygon realPolygon { get; set; }

        public double angle;

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

        public List<MyLine> lines;

        //public GameCanvas gameCanvas; does'nt need game canvas
        public MyLine speedVector;

        public uint FrameHitId = 0;

        //parameters that could be changed

        public double Opacity
        {
            get { return realPolygon.Opacity; }
            set
            {
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

        private Queue<uint> fpsHit= new Queue<uint>(4);

        public MyPolygon(PhysicBody physicBody, double height, double width, double angle = 0, string Id = "") : base(physicBody, height, width)
        {
            //physics
            this.Body = physicBody;
            //size and angle
            this.Height = height;
            this.Width = width;
            this.angle = angle;

            //for better readability
            double x = physicBody.x;
            double y = physicBody.y;

            this.Id = Id;
            
            _pointsImg = CreateRect(x, y, width, height, angle).Points;
            realPolygon = CreateRect(SettingsClass.Convert_To_Real(x), SettingsClass.Convert_To_Real(y), SettingsClass.Convert_To_Real(width), SettingsClass.Convert_To_Real(height), angle);
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
            _pointsImg = RectPoints(Body.x, Body.y, Width, Height, angle);

            UpdateRealSize();
        }

        private Polygon CreateRect(double x, double y, double height, double width, double angle)
        {
            Polygon myPolygon = PolygonAppearance();

            // Define the points of the polygon
            myPolygon.Points = RectPoints(x, y, height, width, angle);

            return myPolygon;
        }

        public Polygon PolygonAppearance()
        {
            return new Polygon()
            {
                Stroke = new SolidColorBrush(Windows.UI.Colors.Purple),
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Windows.UI.Colors.Blue),
                Opacity = 0.7,
            };
        }

        private PointCollection RectPoints(double x, double y, double height, double width, double angle)
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

        public virtual void CreateLineList()
        {
            if (_pointsImg == null) return;//if there isn't a list of points there nothing to draw

            lines = new List<MyLine>();// reset the list

            //this method connects all the point , and leave the last point to connects with first point
            for (int i = 0; i < _pointsImg.Count - 1; i++)
            {
                lines.Add(new MyLine(_pointsImg[i], _pointsImg[i + 1]));
            }
            lines.Add(new MyLine(_pointsImg.Last(), _pointsImg[0]));
        }

        public override CollisionType CollCheck(ReSizable reSizable)
        {
            switch (reSizable)
            {
                case MyPolygon re:

                    return CollCheckForPolygon(re);

                case MyBall ba:

                    return CollCheckForPolygon(ba.Rect);
            }

            return CollisionType.False;
        }

        private CollisionType CollCheckForPolygon(MyPolygon Polygon2)
        {
            if (Polygon2 == null || Polygon2.lines == null || lines == null ) return CollisionType.False;//if there isn't a polygon there isn't collision

            foreach (MyLine line1 in lines)
            {
                foreach (MyLine line2 in Polygon2.lines)
                {
                    PointCol point = line1.Collision(line2);
                    if (point.collation)
                    {
                        return CollisionHandler(Polygon2, line2);
                    }
                }
            }
            return CollisionType.False;

            // crazy one-liner  return (from line1 in lines from line2 in from line2 in Polygon2.lines let point = line1.Collision(line2) where point.collation select line2 select CollisionHandler(Polygon2, line2)).FirstOrDefault();
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

        public  void CollisionRegularLine(MyLine line2)
        {
          /* if (DebugClass.FrameCounter - FrameHitId ==1)//temporary solution
            {
                this.Body.y -= Body.vy/SettingsClass.current_FPS*2;
                this.Body.x -= Body.vx / SettingsClass.current_FPS*2;
            }*/
            FrameHitId = DebugClass.FrameCounter;//current frame
            fpsHit.Enqueue(FrameHitId);
            
            double ang = GetAngleBetweenVectorAndLine(line2.Degree);

            double friction = line2.Friction;//0.8 is for loss of speed when collision 
            DebugClass.angleCollision = ang;

            double rad = SettingsClass.ConvertAngleRadian(ang);

            double vectorValue = Math.Sqrt(Body.vx * Body.vx + Body.vy * Body.vy) * friction;

            Body.vx = vectorValue * Math.Cos(rad);

            Body.vy = vectorValue * Math.Sin(rad);
        }

       

        public void ChangeAppearance(SolidColorBrush solidColorBrush)
        {
            if (solidColorBrush == null) return;
            realPolygon.Fill = solidColorBrush;
        }

        //this change how the block looks
        public void ChangeAppearance(string FileName)
        {
            realPolygon.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Images/" + FileName)),
                Stretch = Stretch.Fill
            };
        }

       

     

      

        public override void AddToCanvas(GameCanvas gameCanvas)
        {
            gameCanvas.AddToCanvas(this);
        }

        public Point ConvertToPoint(double x, double y) => new Point(x, y);

        public Point Convert_To_Real_Point(double x, double y) => new Point((SettingsClass.Convert_To_Real(x)), SettingsClass.Convert_To_Real(y));
    }
}