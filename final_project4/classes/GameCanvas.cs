using final_project4.classes.Shapes;
using System.Collections.Generic;
using System.Threading;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace final_project4.classes
{
    public class GameCanvas 
    {
        public Canvas MainCanvas { get; set; }

        public List<ReSizable> ReList { get; set; }

        public List<MyPolygon> RePolList { get; set; }

        public List<MyBall> ReBallList { get; set; }
       
        public List<MyLine> ReLineList { get; set; }

       int count = 0;



        public GameCanvas(Canvas canvas)
        {
            MainCanvas = canvas;
            ReList = new List<ReSizable>();
            RePolList = new List<MyPolygon>();
            ReBallList = new List<MyBall>();
            ReLineList = new List<MyLine>();
        }

        public void AddToCanvas(ReSizable shape)
        {
            ReList.Add(shape);
          
            switch (shape)
            {
                case MyPolygon pol:
                   // MyPolygon pol = (MyPolygon)shape;
                    MainCanvas.Children.Add(pol.realPolygon);
                    RePolList.Add(pol);
                    break;

                case MyBall ball:
                    //MyBall ball = (MyBall)shape;
                    MainCanvas.Children.Add(ball.BallEllipse);
                    ReBallList.Add(ball);
                    break;
                case MyLine lineCol:
                    MainCanvas.Children.Add(lineCol.line);
                    ReLineList.Add(lineCol);
                    break;

            }



        }

      /*  public void AddToCanvas(MyLine line)
        {
            ReLineList.Add(line);
            MainCanvas.Children.Add(line.line);   
        }*/



        //public update
        public void UpdateObjects()
        {
            if (ReList.Count == 0) return; 
            foreach(var item in ReList)
            {
                if (item != null)
                {
                    item.UpdatePosAndSize();
                }
            }
            foreach (var item in ReLineList)
            {
                if(item != null)
                {
                    item.UpdateLineSize();
                }
            }
        }






        public void MoveAll()
        {
            
            foreach (ReSizable polygon in ReList)
            {
                if (polygon != null)
                {
                    if (polygon.body!=null &&polygon.body.movable)
                    {
                        polygon.body.Move(SettingsClass.current_FPS);

                    }
                    polygon.UpdatePosAndSize();
                }
            }
        }
        public void checkCol()
        {
            for (int i = 0; i < ReList.Count-1; i++)
            {
                for (int j = i+1; j < ReList.Count; j++)
                {
                    if (IsValidCollCheck(i))
                    {
                        switch (CollCheckTwoObjects(ReList[i], ReList[j]))
                        {
                            case CollisionType.Coin:
                                {
                                    ReList[j].height = 0;

                                    break;
                                }
                        }
                        
                    }

                }
            }
            
        }

        private bool IsValidCollCheck(int i)
        {
            return ReList[i] != null && ReList[i].body != null && ReList[i].body.movable == true;
        }

        private CollisionType CollCheckTwoObjects(ReSizable re1, ReSizable re2)
        {
            

            switch (re1)
            {

                case MyPolygon reSizablePolygon:
                    return reSizablePolygon.CollCheck(re2);
                    
                case MyBall reSizableBall:
                    return reSizableBall.CollCheck(re2);
                   
            }

            return CollisionType.False;
        }

    }
}
