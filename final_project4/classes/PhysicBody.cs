using final_project4.classes.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Documents;

namespace final_project4.classes
{
    public class PhysicBody
    {
        public double x,y;
        //Real x that changes accordingly 
        public double xReal => SettingsClass.Convert_To_Real(x);

        //Real y that changes accordingly 
        public double yReal => SettingsClass.Convert_To_Real(y);


        //The speed of the body in for sec 
        public double vy;
        public double vx;

        //The acceleration of the body in sec 
        public double ax;
        public double ay;

        public double[] last4SpeedsVx=new double[4] { 10, 100, 100, 100 };
        public double[] last4SpeedsVy=new double[4] { 100, 100, 100, 100 };


        public double angle
        {
            get
            {
              return SettingsClass.ConvertRadianDegree(Math.Atan(vy / vx));
            }
        }

        public LineCol BodyVector;

        public bool movable;
        public bool HaveGravity;

        
        //const

        public  double gravity = 980;
        public PhysicBody(double x=0 ,double y=0, double vx=0,double vy=0,double ax=0,double ay = 0,bool gravitiy =false,bool movable =false)
        {
            this.x = x;
            this.y = y;

            this.vx = vx;
            this.vy = vy;

            this.ax = ax;
            this.ay = ay;
            HaveGravity = gravitiy;
            if (vx != 0 || vy != 0 || ax != 0 || ay != 0)
            {
                movable = true;
            }
            
            this.movable = movable;
        }

        public  LineCol CreateVectorRepresentation()
        {
            return new LineCol(SettingsClass.PythagoreanTheorem(vy, vx), SettingsClass.ConvertRadianDegree( Math.Atan(vy / vx)), new Windows.Foundation.Point(x, y));
        }
        public LineCol CreateVectorRepresentation(Point point)
        {
            return new LineCol(SettingsClass.PythagoreanTheorem(vy, vx), SettingsClass.ConvertRadianDegree( Math.Atan(vy / vx)), point);
        }

        public void Move(double Fps)
        {
            double dt = 1 / SettingsClass.current_FPS;

            //add a/fps so you move the same if you different fps 
            ChangeSpeed(dt);

            DoesTheBallStop();
            //you move the same in every frame 
            x += vx * dt;
            y += vy * dt;

            UpdateSpeedArr();
            /* if (vx < 0.001)
             {
                 vx = 0;
             }
             if (vy<0.0001)
             {
                 vy = 0;
                 ay = 0;
             }*/

        }

        private void ChangeSpeed(double dt)
        {
            vx += ax * dt;
            if (HaveGravity)
            {
                vy += ay * dt+ gravity *dt;

            }
            else
            {
                vy += ay  * dt;
            }
        }

        public override string ToString()
        {
            return $"({x},{y}), vx:{vx} vy:{vy} ax:{ax} ay:{ay}";
        }
        public void UpdateSpeedArrVx()
        {
            for (int i = 0;i<last4SpeedsVx.Length-1;i++)
            {
                last4SpeedsVx[i] = last4SpeedsVx[i + 1];
            }
            last4SpeedsVx[3] = vx;
        }
        public void UpdateSpeedArrVy()
        {
            /*if (vy==0)
            {
                Console.WriteLine( "0");
            }*/
            for (int i = 0; i < last4SpeedsVy.Length - 1; i++)
            {
                last4SpeedsVy[i] = last4SpeedsVy[i + 1];
            }
            last4SpeedsVy[3] = vy;
        }
        public void UpdateSpeedArr()
        {
            UpdateSpeedArrVx();
            UpdateSpeedArrVy();
        }

        public static double SumArr(double[] arr)
        {
            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum+= arr[i];   
            }
            return sum;
        }
        public void DoesTheBallStop()
        {
            if (Math.Abs( SumArr(last4SpeedsVy))<8)
            {
                vy = 0;
                //HaveGravity = false;
            }
        }

    }
}
