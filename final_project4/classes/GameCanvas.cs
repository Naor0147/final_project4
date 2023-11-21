using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

       public List<ReSizablePolygon> reSizablePolygonList { get; set; }
        int count = 0;



        public GameCanvas(Canvas canvas)
        {
            MainCanvas = canvas;
            reSizablePolygonList = new List<ReSizablePolygon>();
        }

        public void AddToCanvas(ReSizablePolygon polygon)
        {
            reSizablePolygonList.Add(polygon);
            polygon.AddToCanvas(MainCanvas);
            count++;
        }

        //public update


        public void MoveAll()
        {
            
            foreach (ReSizablePolygon polygon in reSizablePolygonList)
            {
                
                polygon.body.Move(SettingsClass.current_FPS);
                polygon.UpdateImgSize();
            }
        }
        public bool checkCol()
        {
            for (int i = 0; i < reSizablePolygonList.Count-1; i++)
            {
                for (int j = i+1; j < reSizablePolygonList.Count; j++)
                {
                    if (reSizablePolygonList[i].CollCheck(reSizablePolygonList[j]))
                    {
                        return true;
                    } 
                }
            }
            return false;
        }
        
        
    }
}
