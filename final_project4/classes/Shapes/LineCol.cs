
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Devices.PointOfService;
using Windows.Foundation;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace final_project4.classes.Shapes
{
    public enum LineType
    {
        VerticalLine,// x=value
        RegularLine,//y= mx+b 

    }
    public class LineCol
    {
        double x1, y1, x2, y2;

        public double m, b; // y=mx+b

        string function;//for better readiblty 

        
        public LineType _LineType { get; set; }

        private Line line;



        public LineCol(Point p1,Point p2)
        {
            createLine(p1.X,p1.Y,p2.X,p2.Y);
        }
      
        public LineCol(double x1,double y1, double x2 ,double y2)
        {
            createLine(x1, y1, x2, y2);

        }
        public LineCol(double VectorMagnitude ,double angle,Point point)
        {
            
            angle= SettingsClass.ConvertAngleRadian(angle);
            double dx = VectorMagnitude*Math.Cos(angle);
            double dy= VectorMagnitude*Math.Sin(angle);
            createLine(point.X, point.Y, point.X + dx, point.Y + dy);
        }

        private void createLine(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;


            //default values in struct
            function = "";
            _LineType = LineType.RegularLine;
            m = 0;
            b = 0;


            ConvertData();
        }


        public double GetDistance()
        {
            return Math.Sqrt(Power2(x2 - x1) - Power2(y2 - y1));
        }
        public double Power2(double v) => v * v;


        public  void ConvertData()
        {
            if (x1==x2)
            {
                _LineType = LineType.VerticalLine;

                function = $"x={x1}";
                return;
            }
            _LineType = LineType.RegularLine;
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
        public double Get_Y_Value_On_X(double x_temp)
        {
            return m * x_temp + b;
        }

        //working 
        public PointCol Collision(LineCol line)
        {

            

            bool line1Vertical = this._LineType == LineType.VerticalLine;
            bool line2Vertical = line._LineType == LineType.VerticalLine;


            if (line1Vertical || line2Vertical)
            {
                return CollisionForVerticalLines(line);
            }
            
            
            if (this.m == line.m)
            {
                if (x1==x2)// if the line are x=value and not y=mx+b 
                {
                    if ((x1 == line.x1) && (SettingsClass.isBetween(y1, line.y1, y2) || SettingsClass.isBetween(y1, line.y2, y2)))
                    {
                        return new PointCol(x1, ((y2 - y1) / 2 + y1), 90);// gets the point that in the middle of the rect

                    }
                    return new PointCol();// if there isn't collision

                }
                if (this.b == line.b)
                {
                    if (SettingsClass.isBetween(x1, line.x1, x2) || SettingsClass.isBetween(x1, line.x2, x2))
                    {
                        //because it is the same function ,
                        //just with a different limit there is things we need to check
                        //we need to check what points are on the line 
                        // Overlapping segment, find the middle point
                        return GetMiddlePoint(x1,x2,line.x1,line.x2);
                    }

                }
                    return new PointCol();// if there isn't collision
                
            }


            double x = (line.b - b) / (m - line.m);
            double y = Get_Y_Value_On_X(x);
           // Point_f col = new Point_f(x, y);
            Point col= new Point(x,y);

            bool condition1 = SettingsClass.isBetween(x1, x, x2);
            bool condition2 = SettingsClass.isBetween(line.x1, x, line.x2);

            
            return new PointCol(x,y, (condition1 && condition2));

        }

        private PointCol GetMiddlePoint(double n1,double n2,double n3,double n4)
        {
            double[] arr = new double[] { n1, n2, n3, n4 };
            Array.Sort(arr);
            double tempX = (arr[2] - arr[1]) / 2 + arr[0];
            return new PointCol(tempX, Get_Y_Value_On_X(tempX));
        }

        // <image src="C:\Vs_Projects\Final_Projects\Project1\final_project4\final_project4\Images\Screenshot 2023-12-22 091410.png" scale="0.5" /> 

        private PointCol CollisionForVerticalLines(LineCol line)
        {
            bool line1Vertical = this._LineType == LineType.VerticalLine;
            bool line2Vertical = line._LineType == LineType.VerticalLine;
            /* if two lines are vertical , you need to check if two lines have the same x=value
             and if they are they you need to check if the one of the two lines point are found between those two lines */
            if (line1Vertical && line2Vertical)
            {
                if ((x1 == line.x1) && (SettingsClass.isBetween(y1, line.y1, y2) || SettingsClass.isBetween(y1, line.y2, y2)))
                {
                    return GetMiddlePoint(y1,y2,line.y1,line.y2);
                }
                return new PointCol();
            }
            //reaccuse it a continuous line line there got a be Collision in the x of the straight line 
            return CollisionVerticalWithNormalLine(this, line);
        }

        private PointCol CollisionVerticalWithNormalLine(LineCol line1, LineCol line2)
        {
            LineCol line;
            if (line2._LineType== LineType.VerticalLine)
            {
                line = line1;
                line1 = line2;
                line2 = line;
            }
            double _x = line1.x1;
            double _y = line2.Get_Y_Value_On_X(x1);// one is vertical one line is normal , so there  meeting point on x must be the same , dosent work properly 
            return new PointCol(_x,_y, SettingsClass.isBetween(line2.x1, _x, x2) && (SettingsClass.isBetween(line2.y1, _y, line2.y2)));
            
        }

        public void DrawLine(GameCanvas canvas)
        {
            line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
            };
            line.Stroke= new SolidColorBrush(Windows.UI.Colors.Black);
            line.StrokeThickness = 5;
            canvas.AddToCanvas(line);
        }
        
        public void UpdateLineSize()
        {
            line.X1 = SettingsClass.Convert_To_Real(x1);
            line.Y1 = SettingsClass.Convert_To_Real(y1);
            line.X2 = SettingsClass.Convert_To_Real(x2);
            line.Y2 = SettingsClass.Convert_To_Real(y2);
        }
    }
}
