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


        //The speed of the body in for sec 
        public double vy;
        public double vx;

        //The acceleration of the body in sec 
        public double ax;
        public double ay;


        //const

        public const double gravity = 9.8;
        public PhysicBody(double x ,double y, double vx=0,double vy=0,double ax=0,double ay = 0)
        {
            this.x = x;
            this.y = y;
                
            this.vx = vx;
            this.vy = vy;
                
            this.ax = ax;
            this.ay = ay;
        }

        public void Move(double Fps)
        {
            double dt = 1 / SettingsClass.current_FPS;

            //add a/fps so you move the same if you diffrent fps 
            vx += ax * dt;
            vy += ay * dt;

            //you move the same in every frame 
            x+= vx * dt;
            y+= vy * dt;

        }


    }
}
