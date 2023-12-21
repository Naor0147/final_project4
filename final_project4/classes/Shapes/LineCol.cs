﻿
using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool Collision(LineCol line)
        {

            

            bool line1Vertical = this._LineType == LineType.VerticalLine;
            bool line2Vertical = line._LineType == LineType.VerticalLine;


            if (line1Vertical || line2Vertical)
            {
                return CollisionForVerticalLines(line, line1Vertical, line2Vertical);
            }
            
            
            if (this.m == line.m)
            {
                if (x1==x2)// if the line are x=value and not y=mx+b 
                {
                    return (x1 == line.x1) && (SettingsClass.isBetween(y1, line.y1, y2) || SettingsClass.isBetween(y1, line.y2, y2));
                }
                if (this.b == line.b)
                {
                    return SettingsClass.isBetween(x1, line.x1, x2) || SettingsClass.isBetween(x1, line.x2, x2);
                }
            }


            double x = (line.b - b) / (m - line.m);
            double y = Get_Y_Value_On_X(x);
           // Point_f col = new Point_f(x, y);
            Point col= new Point(x,y);

            bool condition1 = SettingsClass.isBetween(x1, x, x2);
            bool condition2 = SettingsClass.isBetween(line.x1, x, line.x2);

         
            return (condition1 && condition2);

        }


        // <image src="C:\Vs_Projects\Final_Projects\Project1\final_project4\final_project4\Images\garrett-parker-DlkF4-dbCOU-unsplash.jpg" scale="0.1" /> 
        private bool CollisionForVerticalLines(LineCol line, bool line1Vertical, bool line2Vertical)
        {
            /* if two lines are vertical , you need to check if two lines have the same x=value
             and if they are they you need to check if the one of the two lines point are found between those two lines */
            if (line1Vertical && line2Vertical)
            {
                return (x1 == line.x1) && (SettingsClass.isBetween(y1, line.y1, y2) || SettingsClass.isBetween(y1, line.y2, y2));
            }
            //reaccuse it a continuous line line there got a be Collision in the x of the straight line 
            return CollisionVerticalWithNormalLine(this, line);
        }

        private bool CollisionVerticalWithNormalLine(LineCol line1, LineCol line2)
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
            return SettingsClass.isBetween(line2.x1,_x,x2)&&(SettingsClass.isBetween(line2.y1, _y, line2.y2));
        }

        public void drawLine(GameCanvas canvas)
        {
            Line line = new Line()
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
    }
}
