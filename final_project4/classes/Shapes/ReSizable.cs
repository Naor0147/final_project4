using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project4.classes.Shapes
{
    public class ReSizable
    {
        public PhysicBody body;
        public double width,height;
        public GameCanvas GameCanvas;

        public ReSizable(PhysicBody body,double width,double height ,GameCanvas gameCanvas)
        {
            this.body = body;
            this.width = width;
            this.height = height;
            this.GameCanvas = gameCanvas;
        }

        public virtual void UpdatePosAndSize() { }

        public virtual void AddToCanvas() { }
        
        public virtual void CollCheck() { }
        public virtual void CollCheck(ReSizablePolygon reSizablePolygons) { }
        public virtual void CollCheck(ReSizableBall reSizableBall) { }



    }
}
