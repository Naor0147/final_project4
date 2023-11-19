using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace final_project4.classes
{
    class ReSizablePolygon
    {
        public Polygon imgPolygon { get; set; }

        public Polygon realPolygon{ get; set; }
        public double height, width, angle;
        public PhysicBody body;


        public ReSizablePolygon(PhysicBody physicBody,double height ,double width,double angle)
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

            imgPolygon = CreateRect(x,y,width,height,angle);
            realPolygon= CreateRect(SettingsClass.Convert_To_Real(x),SettingsClass.Convert_To_Real(y), SettingsClass.Convert_To_Real(width), SettingsClass.Convert_To_Real(height), angle);
        }
        public void UpdateRealSize()
        {
          
            for (int i = 0; i < imgPolygon.Points.Count; i++)
            {
                realPolygon.Points[i] = Convert_To_Real_Point(imgPolygon.Points[i].X, imgPolygon.Points[i].Y);
            }

            
        }
        


        public void UpdateImgSize()
        {
            imgPolygon.Points = RectPoints(body.x,body.y,width,height,angle);
            UpdateRealSize();
        }


        public void AddToCanvas(Canvas canvas)
        {
            canvas.Children.Add(realPolygon);
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
                StrokeThickness = 6,
                Fill = new SolidColorBrush(Windows.UI.Colors.Blue) ,
                Opacity =0.4,
            };

        }

        private PointCollection RectPoints(double x, double y, double height, double width,double angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            PointCollection polygonPoints = new PointCollection();
            polygonPoints.Add(ConvertToPoint(x, y));
            polygonPoints.Add(ConvertToPoint((x - height * sin), (y + height * cos)));
            polygonPoints.Add(ConvertToPoint((x + width * cos - height * sin), (y + width * sin + height * cos)));
            polygonPoints.Add(ConvertToPoint((x + width * cos), (y + width * sin)));
            return polygonPoints;
        }

        public Windows.Foundation.Point ConvertToPoint(double x, double y)=> new Windows.Foundation.Point((int)x, (int)y);
        public Windows.Foundation.Point Convert_To_Real_Point(double x, double y) => new Windows.Foundation.Point((int)(SettingsClass.Convert_To_Real(x)),(int)SettingsClass.Convert_To_Real(y));


    }
}
