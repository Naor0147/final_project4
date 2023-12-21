using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool movable;
        //const

        public const double gravity = 9.8;
        public PhysicBody(double x ,double y, double vx=0,double vy=0,double ax=0,double ay = 0,bool movable =false)
        {
            this.x = x;
            this.y = y;
                
            this.vx = vx;
            this.vy = vy;
                
            this.ax = ax;
            this.ay = ay;
            if (vx!=0||vy!=0||ax!=0||ay!=0)
            {
                movable = true;
            }
            this.movable=movable;
        }

        public void Move(double Fps)
        {
            double dt = 1 / SettingsClass.current_FPS;

            //add a/fps so you move the same if you different fps 
            vx += ax * dt;
            vy += ay * dt;

            //you move the same in every frame 
            x+= vx * dt;
            y+= vy * dt;

        }

        public override string ToString()
        {
            return $"({x},{y}), vx:{vx} vy:{vy} ax:{ax} ay:{ay}";
        }

    }
}
