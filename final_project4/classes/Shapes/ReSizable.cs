using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project4.classes.Shapes
{
    public enum CollisionType
    {
        False,Wall,Coin,Win
    }


    public abstract class ReSizable
    {
        public PhysicBody body;
        public double width,height;
        

        public ReSizable(PhysicBody body,double width=0,double height=0 )
        {
            this.body = body;
            this.width = width;
            this.height = height;
           
        }
        public ReSizable()
        {
            this.width = 0;
            this.height =0;
        }

        public virtual void UpdatePosAndSize() { }

        public virtual void AddToCanvas(GameCanvas gameCanvas) { }
        
        public virtual CollisionType CollCheck(ReSizable reSizableBall) {
            return CollisionType.False;

        }
        //public virtual void CollCheck(MyPolygon reSizablePolygons) { }
        //public virtual void CollCheck(MyBall reSizableBall) { }

       // public virtual bool collCheckForPolygon( MyBall reSizableBall) {}
        




    }
}
