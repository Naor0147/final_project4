using final_project4.classes.Shapes;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace final_project4.classes
{
    public class GameCanvas 
    {
        public Canvas MainCanvas { get; set; }

        public List<ReSizable> reList { get; set; }

        public List<ReSizablePolygon> rePolList { get; set; }

        public List<ReSizableBall> reBallList { get; set; }
       
        public List<LineCol> ReLineList { get; set; }

       int count = 0;



        public GameCanvas(Canvas canvas)
        {
            MainCanvas = canvas;
            reList = new List<ReSizable>();
            rePolList = new List<ReSizablePolygon>();
            reBallList = new List<ReSizableBall>();
            ReLineList = new List<LineCol>();
        }

        public void AddToCanvas(ReSizable shape)
        {
            reList.Add(shape);
          
            switch (shape)
            {
                case ReSizablePolygon re:
                    ReSizablePolygon pol = (ReSizablePolygon)shape;
                    MainCanvas.Children.Add(pol.realPolygon);
                    rePolList.Add(pol);
                    break;

                case ReSizableBall ba:
                    ReSizableBall ball = (ReSizableBall)shape;
                    MainCanvas.Children.Add(ball.realEllipse);
                    reBallList.Add(ball);
                    break;

            }



        }

        public void AddToCanvas(LineCol line)
        {
            ReLineList.Add(line);
            MainCanvas.Children.Add(line);   
        }



        //public update
        public void UpdateObjects()
        {
            if (reList.Count == 0) return; 
            foreach(var item in reList)
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
                    item.
                }
            }
        }






        public void MoveAll()
        {
            
            foreach (ReSizable polygon in reList)
            {
                if (polygon != null)
                {

                    polygon.body.Move(SettingsClass.current_FPS);
                    polygon.UpdatePosAndSize();
                }
            }
        }
        public bool checkCol()
        {
            for (int i = 0; i < reList.Count-1; i++)
            {
                for (int j = i+1; j < reList.Count; j++)
                {
                    if (reList[i]!=null &&reList[i].body.movable== true)
                    {
                        if (reCheck(reList[i], reList[j]))
                        {
                            return true;
                        }
                    }

                }
            }
            return false;
        }

        private bool reCheck(ReSizable re1, ReSizable re2)
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
