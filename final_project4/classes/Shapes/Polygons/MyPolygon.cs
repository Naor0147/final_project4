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

                UpdateLinesList();
            }
        }

        public List<MyLine> lines;

        //public GameCanvas gameCanvas; does'nt need game canvas
        public MyLine speedVector;


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

        public uint FrameHitId = 0;

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
            /// if i put the angle to a really small number close to 0 , collision will work properly , need to if there is posiible way to fix
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


        public virtual void UpdateLinesList()
        {
            if (_pointsImg == null) return;
            if (lines== null || lines.Count == 0) { CreateLineList(); }
            for (int i = 0; i < _pointsImg.Count - 1; i++)
            {
                lines[i].x1 = _pointsImg[i].X;
                lines[i].y1 = _pointsImg[i].Y;
                lines[i].x2 = _pointsImg[i+1].X;
                lines[i].y2= _pointsImg[i+1].Y;
            }
            lines.Last().x1 = _pointsImg.Last().X;
            lines.Last().y1 = _pointsImg.Last().Y;
            lines.Last().x2 = _pointsImg[0].X;
            lines.Last().y2= _pointsImg[0].Y;


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
            //CreateText();
        }
        public void CreateText()
        {
            foreach(MyLine line in lines)
            {
                line.addText();
            }
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