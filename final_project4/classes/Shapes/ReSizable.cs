using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project4.classes.Shapes
{
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
            this.body = new PhysicBody();
            this.width = 0;
            this.height =0;
        }

        public virtual void UpdatePosAndSize() { }

        public virtual void AddToCanvas(GameCanvas gameCanvas) { }
        
        public virtual bool CollCheck(ReSizable reSizableBall) {
            return false;

        }
        //public virtual void CollCheck(ReSizablePolygon reSizablePolygons) { }
        //public virtual void CollCheck(ReSizableBall reSizableBall) { }

       // public virtual bool collCheckForPolygon( ReSizableBall reSizableBall) {}
        




    }
}
