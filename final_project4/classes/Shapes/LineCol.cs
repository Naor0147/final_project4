
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Shapes;

namespace final_project4.classes.Shapes
{
    enum LineType
    {
        XType,// x=value
        YType,//y= mx+b 

    }
    public struct LineCol
    {
        double x1, y1, x2, y2;

        double m, b; // y=mx+b

        string function;//for better readiblty 

        private LineType _LineType { get; set; }


        

        public LineCol(Point p1,Point p2)
        {
            x1 = p1.X;
            y1 = p1.Y;
            x2 = p2.X;
            y2 = p2.Y;
            function = "";
            _LineType = LineType.YType;
            ConvertData();
        }
      
        public LineCol(double x1,double y1, double x2 ,double y2) 
        { 
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            ConvertData();


        }
        public  void ConvertData()
        {
            if (x1==x2)
            {
                _LineType = LineType.XType;

                function = $"x={x1}";
                return;
            }
            _LineType = LineType.YType;
            m =(y1-y2)/(x1-x2);
            
            //the y value when x=0 
            this.b = y1 - (m * x1);
            //convert to function 
            function = $"y= {m}*x";
            if (b > 0)
            {
                function += " +" + b;
            }
            else if (b < 0)
            {
                function += " -" + b;
            }
            
        }

        public bool Collision(LineCol line)
        {
            {
                bool line1Vertical = this._LineType == LineType.XType;
                bool line2Vertical = line._LineType == LineType.XType;


                if (line1Vertical || line2Vertical)
                {
                    if (line1Vertical && line2Vertical)
                    {
                        return (x1 == line.x1) && (SettingsClass.isBetween(y1, line.y1, y2) || SettingsClass.isBetween(y1, line.y2, y2));
                    }
                    else if (line1Vertical)
                    {
                        double _y = line.Get_Y_Value_On_X(x1);// one is vertical one line is normal , so there  meeting point on x must be the same 
                        double _x = x1;

                    }
                }
            }


            if (this.m == line.m)
            {
                if (x1==x2)// if the line are x=value and not y=mx+b 
                {
                    return (x1 == line.x1) && (SettingsClass.isBetween(y1, line.y1, y2) || SettingsClass.isBetween(y1, line.y2, y2));
                }
                if (this.b == line.b)
                {
                    return true;//the same line
                }
                return false;
            }


            double x = (line.b - b) / (m - line.m);
            double y = Get_Y_Value_On_X(x);
           // Point_f col = new Point_f(x, y);
            Point col= new Point(x,y);

            bool condition1 = SettingsClass.isBetween(x1, x, x2);
            bool condition2 = SettingsClass.isBetween(line.x1, x, line.x2);

            /*pretty sure i dont need this check 
             * cause i allready cheked the x of the line so i dont need to check the y 
             * 
             bool condtion3 = Settings_class.isBetween(y1, y, y2);
            bool condtion4 = Settings_class.isBetween(line.y1, y, line.y2);
            return (condtion1 && condtion2 && condtion3 && condtion4);*/

            return (condition1 && condition2);

        }
        public double Get_Y_Value_On_X(double x_temp)
        {
            return m * x_temp + b;
        }

    }
}
