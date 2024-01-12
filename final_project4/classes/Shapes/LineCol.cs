
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
    
    public class LineCol:ReSizable
    {
        private double _x1, _y1, _x2, _y2;
        public double x1
        {
            get
            {
                return _x1;
            }
            set
            {
                _x1 = value;
                updateLine();
            }
        }
        public double y1
        {
            get
            {
                return _y1;
            }
            set
            {
                _y1 = value;
                updateLine();
            }
        }
        public double x2
        {
            get
            {
                return _x2;
            }
            set
            {
                _x2 = value;
                updateLine();
            }
        }
        public double y2
        {
            get
            {
                return _y2;
            }
            set
            {
                _y2 = value;
                updateLine();
            }
        }

        public double m, b; // y=mx+b

        string Function;//for better readiblty 



        //the line as a vector
        private double _degree;
        public double Degree
        {
            get { return _degree; }
            set
            {
                _degree = value;
                _radian = SettingsClass.ConvertAngleRadian(value);
               

            }
        }
        public double _radian;
        public double Radian
        {
            get { return _radian; }
            set
            {
                _radian = value;
                _degree= SettingsClass.ConvertRadianDegree(value);
                
                //need to update all the other variable x1 ,x2 ...
            }
        }

        public double VectorMagnitude;
        


     

        public Line line;



        public LineCol(Point p1,Point p2):base()
        {
            ConvertLineToVector(p1.X, p1.Y, p2.X, p2.Y);

            CreateLine(p1.X,p1.Y,p2.X,p2.Y);
        }
      
        public LineCol(double x1,double y1, double x2 ,double y2) : base()
        {
            ConvertLineToVector(x1 ,y1 ,x2,y2); 
            CreateLine(x1, y1, x2, y2);

        }
        public LineCol(double VectorMagnitude ,double angle,Point point) : base()
        {
            ConvertVectorToLine(VectorMagnitude, angle, point);
            //ConvertLineToVector(x1, y1, x2, y2);
        }

        //i put id just for makeing diffreant fuctions
        public LineCol(double vx, double vy, Point point , int id) : base()
        {
            ConvertSpeedToLineCol(vx, vy, point);
        }

        private void ConvertVectorToLine(double VectorMagnitude, double angle, Point point)
        {
            Degree = angle;// the radian is set automatically 
            double dx = VectorMagnitude * Math.Cos(Radian);
            double dy = VectorMagnitude * Math.Sin(Radian);
            CreateLine(point.X, point.Y, point.X + dx, point.Y + dy);
        }
        private void ConvertSpeedToLineCol(double vx ,double vy ,Point point)
        {
            double _angle =180- Math.Atan(vy / vx);
            double _VectorMagnitude =SettingsClass.PythagoreanTheorem(vx,vy);

            double dx = _VectorMagnitude * Math.Cos(_angle);
            double dy = _VectorMagnitude * Math.Sin(_angle);
            CreateLine(point.X, point.Y, point.X+  dx, point.Y + dy);
        }


        private void updateLine()
        {
            CreateLine(x1, y1, x2, y2);
            ConvertLineToVector(x1, y1, x2, y2);
        }

        private void ConvertLineToVector(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            if (dx==0)
                Radian = 0;

            else
                Radian = Math.Atan((dy)/(dx));

            VectorMagnitude=SettingsClass.PythagoreanTheorem(dy,dx);
        }
        private void CreateLine(double x1, double y1, double x2, double y2)
        {
            this._x1 = x1;
            this._y1 = y1;
            this._x2 = x2;
            this._y2 = y2;






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
                //convert to Function 
                Function = $"x={x1}";
                x2 += 0.00001;
                /*make a f(x)= x to f(x) =mx+b , 
                 but still look the same so i don't need to check edge cases . 
                */
                return;
            }
            
            m =(y1-y2)/(x1-x2);
            
            //the y value when x=0 
            this.b = y1 - (m * x1);
            
            Function = $"y= {m}*x";

            if (b > 0)
            {
                Function += " +" + b;
            }
            else if (b < 0)
            {
                Function += " -" + b;
            }
            
        }
        public double Get_Y_Value_On_X(double x_temp)
        {
            return m * x_temp + b;
        }

        //working 
        public PointCol Collision(LineCol line)
        {

            

          
            
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
                        //because it is the same Function ,
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

        public void CreateLineVisualization()
        {
            line = new Line()
            {
                X1 = SettingsClass.Convert_To_Real(x1),
                Y1 = SettingsClass.Convert_To_Real(y1),
                X2 = SettingsClass.Convert_To_Real(x2),
                Y2 = SettingsClass.Convert_To_Real(y2)
            };
            line.Stroke= new SolidColorBrush(Windows.UI.Colors.Black);
            line.StrokeThickness = 5;
        }
        public override void AddToCanvas(GameCanvas gameCanvas)
        {
            CreateLineVisualization();
            gameCanvas.AddToCanvas(this);
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
