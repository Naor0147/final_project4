﻿using final_project4.classes.Shapes;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace final_project4.classes
{
    public class GameCanvas
    {
        public Canvas MainCanvas { get; set; }

       public List<ReSizable> reList { get; set; }

       public List<ReSizablePolygon> rePolList { get; set; }

       public List<ReSizableBall> reBallList { get; set; }

       int count = 0;



        public GameCanvas(Canvas canvas)
        {
            MainCanvas = canvas;
            reList = new List<ReSizable>();
            rePolList = new List<ReSizablePolygon>();
            reBallList = new List<ReSizableBall>();
        }

        public void AddToCanvas(ReSizable polygon)
        {
            reList.Add(polygon);
          
            switch (polygon)
            {
                case ReSizablePolygon re:
                    ReSizablePolygon pol = (ReSizablePolygon)polygon;
                    MainCanvas.Children.Add(pol.realPolygon);
                    rePolList.Add(pol);
                    break;

                case ReSizableBall ba:
                    ReSizableBall ball = (ReSizableBall)polygon;
                    MainCanvas.Children.Add(ball.realEllipse);
                    reBallList.Add(ball);
                    break;

            }



            count++;
        }

        //public update
        public void UpdateObjects()
        {
            foreach(var item in reList)
            {
                item.UpdatePosAndSize();  
            }
        }






        public void MoveAll()
        {
            
            foreach (ReSizablePolygon polygon in rePolList)
            {
                polygon.body.Move(SettingsClass.current_FPS);
                polygon.UpdatePosAndSize();
            }
        }
        public bool checkCol()
        {
            for (int i = 0; i < reList.Count-1; i++)
            {
                for (int j = i+1; j < reList.Count; j++)
                {
                    if (reCheck(reList[i], reList[j]))
                    {
                        return true;
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
