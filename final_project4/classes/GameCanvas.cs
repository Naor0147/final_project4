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

        public List<ReSizablePolygon> RePolList { get; set; }

        public List<ReSizableBall> ReBallList { get; set; }
       
        public List<LineCol> ReLineList { get; set; }

       int count = 0;



        public GameCanvas(Canvas canvas)
        {
            MainCanvas = canvas;
            ReList = new List<ReSizable>();
            RePolList = new List<ReSizablePolygon>();
            ReBallList = new List<ReSizableBall>();
            ReLineList = new List<LineCol>();
        }

        public void AddToCanvas(ReSizable shape)
        {
            ReList.Add(shape);
          
            switch (shape)
            {
                case ReSizablePolygon pol:
                   // ReSizablePolygon pol = (ReSizablePolygon)shape;
                    MainCanvas.Children.Add(pol.realPolygon);
                    RePolList.Add(pol);
                    break;

                case ReSizableBall ball:
                    //ReSizableBall ball = (ReSizableBall)shape;
                    MainCanvas.Children.Add(ball.realEllipse);
                    ReBallList.Add(ball);
                    break;
                case LineCol lineCol:
                    MainCanvas.Children.Add(lineCol.line);
                    ReLineList.Add(lineCol);
                    break;

            }



        }

      /*  public void AddToCanvas(LineCol line)
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
        public bool checkCol()
        {
            for (int i = 0; i < ReList.Count-1; i++)
            {
                for (int j = i+1; j < ReList.Count; j++)
                {
                    if (IsValidCollCheck(i))
                    {
                        CollCheckTwoObjects(ReList[i], ReList[j]);
                        
                        
                    }

                }
            }
            return false;
        }

        private bool IsValidCollCheck(int i)
        {
            return ReList[i] != null && ReList[i].body != null && ReList[i].body.movable == true;
        }

        private bool CollCheckTwoObjects(ReSizable re1, ReSizable re2)
        {
            

            switch (re1)
            {

                case ReSizablePolygon reSizablePolygon:
                    return reSizablePolygon.CollCheck(re2);
                    
                case ReSizableBall reSizableBall:
                    return reSizableBall.CollCheck(re2);
                   
            }

            return false;
        }

    }
}
