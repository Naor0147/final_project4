using final_project4.classes.Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
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

       public List<ReSizable> reSizablePolygonList { get; set; }

       public List<ReSizablePolygon> reSizablePolygonsList { get; set; }
       int count = 0;



        public GameCanvas(Canvas canvas)
        {
            MainCanvas = canvas;
            reSizablePolygonList = new List<ReSizable>();
        }

        public void AddToCanvas(ReSizable polygon)
        {
            reSizablePolygonList.Add(polygon);
            if (polygon is ReSizablePolygon)
            {
                ReSizablePolygon add= (ReSizablePolygon)polygon;
                add.AddToCanvas();
            }
            count++;
        }

        //public update
        public void UpdateObjects()
        {
            foreach(var item in reSizablePolygonList)
            {
                item.UpdatePosAndSize();  
            }
        }






        public void MoveAll()
        {
            
            foreach (ReSizablePolygon polygon in reSizablePolygonList)
            {
                
                polygon.body.Move(SettingsClass.current_FPS);
                polygon.UpdatePosAndSize();
            }
        }
        public bool checkCol()
        {
            for (int i = 0; i < reSizablePolygonList.Count-1; i++)
            {
                for (int j = i+1; j < reSizablePolygonList.Count; j++)
                {
                    bool temp = false;

                    switch (reSizablePolygonList[i])
                    {
                        case ReSizablePolygon reSizablePolygon:
                            temp= reSizablePolygon.CollCheck(reSizablePolygonList[j]);
                            break;
                        case ReSizableBall reSizableBall:
                            temp= reSizableBall.CollCheck(reSizablePolygonList[j]);
                            break;
                    }

                    if (temp)
                    {
                        return true;
                    }


                   
                   
                    
                }
            }
            return false;
        }
        
        
    }
}
