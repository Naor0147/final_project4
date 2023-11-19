using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace final_project4.classes
{
    public class GameCanvas
    {
        public Canvas MainCanvas { get; set; }

        private List<Polygon> polygonsImg = new List<Polygon>();
        //need to create  a clone not 
        private List<Polygon> polygonsReal = new List<Polygon>();




        public GameCanvas(Canvas canvas)
        {
            MainCanvas = canvas;
        }

        public void AddToCanvas(Polygon polygon)
        {

            
            polygonsImg.Add(polygon);
            polygonsReal.Add(ClonePolygon(polygon));
            MainCanvas.Children.Add(polygon);
        }

        private Polygon ClonePolygon(Polygon originalPolygon)
        {
            var clonedPolygon = new Polygon
            {
                Fill = originalPolygon.Fill,
                Stroke = originalPolygon.Stroke,
                StrokeThickness = originalPolygon.StrokeThickness,
                FillRule = originalPolygon.FillRule,

            };

            foreach (var point in originalPolygon.Points)
            {
                clonedPolygon.Points.Add(point);
            }

            return clonedPolygon;
        }


        public void MoveAll(int dx, int dy) { 
            
            foreach (var polygon in polygonsImg)
            {
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new Windows.Foundation.Point(polygon.Points[i].X+dx, polygon.Points[i].Y + dy);
                }
                
            }
        }
        public void UpdateSize(int dx, int dy)
        {
            foreach (var polygon in polygonsReal)
            {
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new Windows.Foundation.Point(polygon.Points[i].X + dx, polygon.Points[i].Y + dy);
                }

            }
        }
         

        public void RemoveFromCanvas(Polygon polygon)
        {
            polygonsImg.Remove(polygon);
            polygonsReal.Remove(polygon);
            MainCanvas.Children.Remove(polygon);
        }
    }
}
