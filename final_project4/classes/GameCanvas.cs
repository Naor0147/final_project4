using final_project4.classes.Shapes;
using final_project4.classes.Shapes.Polygons;
using final_project4.classes.Stats;
using NetTopologySuite.Index.HPRtree;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace final_project4.classes
{
    public class GameCanvas
    {
        public Canvas MainCanvas { get; set; }

        public List<ReSizable> ReList { get; set; }

        public List<MyLine> ReLineList { get; set; }

        public List<MyText> MyTextList { get; set; }

        public MyBall MyBall { get; set; }

       

        public GameCanvas(Canvas MainCanvas)
        {
            this.MainCanvas = MainCanvas;

            //where i save the player level stats
            
            //Lists 
            ReList = new List<ReSizable>();
            ReLineList = new List<MyLine>();
            MyTextList = new List<MyText>();

            
        }

       
      

        public void AddToCanvas<T>(T shape)
        {
            if (shape is ReSizable && !(shape is MyBall))
            {
                ReList.Add(shape as ReSizable);
            }

            switch (shape)
            {
                case MyPolygon pol:

                    MainCanvas.Children.Add(pol.realPolygon);

                    break;

                case MyBall ball:

                    MainCanvas.Children.Add(ball.BallEllipse);
                    this.MyBall = ball;
                    break;

                case MyLine lineCol:
                    MainCanvas.Children.Add(lineCol.line);
                    ReLineList.Add(lineCol);
                    break;

                case MyText text:
                    MainCanvas.Children.Add(text.TextBlock);
                    MyTextList.Add(text);
                    break;
                case MyButton button:
                    MainCanvas.Children.Add(button.Button);
                    break;
            }
        }

        public void removeObject<T>(T shape)
        {
            if (shape is ReSizable && !(shape is MyBall))
            {
                ReList.Remove(shape as ReSizable);
            }

            switch (shape)
            {
                case MyPolygon pol:

                    MainCanvas.Children.Remove(pol.realPolygon);

                    break;

                case MyBall ball:

                    MainCanvas.Children.Remove(ball.BallEllipse);
                    this.MyBall = null;
                    break;

                case MyLine lineCol:
                    MainCanvas.Children.Remove(lineCol.line);
                    ReLineList.Remove(lineCol);
                    break;

                case MyText text:
                    MainCanvas.Children.Remove(text.TextBlock);
                    MyTextList.Remove(text);
                    break;
                case MyButton button:
                    MainCanvas.Children.Remove(button.Button);
                    break;
            }
        }

        //public update
        public void UpdateObjects()
        {
            if (MyBall !=null)
            {
                 MyBall.UpdatePosAndSize();

            }
            if (ReList.Count == 0) return;
            foreach (var item in ReList)
            {
                if (item == null) continue;

                item.UpdatePosAndSize();
            }
            foreach (var item in ReLineList)
            {
                if (item == null) continue;

                item.UpdateLineSize();
            }

           
        }

        public void MoveAll()
        {
            //foreach (ReSizable polygon in ReList)
            foreach (ReSizable polygon in ReList.Where(polygon => polygon != null))
            {

                MoveObject(polygon);
            }
            MoveObject(MyBall);

        }

        private static void MoveObject(ReSizable polygon)
        {
            if (polygon != null &&polygon.Body != null && polygon.Body.Movable)
            {
                polygon.Body.Move(SettingsClass.current_FPS);
            }
            polygon.UpdatePosAndSize();
        }




        public virtual void Functions()
        {
            

            MoveAll();

            UpdateObjects();
        }
    }
}