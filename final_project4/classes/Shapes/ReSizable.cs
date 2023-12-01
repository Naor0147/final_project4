﻿using System;
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
        

        public ReSizable(PhysicBody body,double width,double height )
        {
            this.body = body;
            this.width = width;
            this.height = height;
           
        }

        public virtual void UpdatePosAndSize() { }

        public virtual void AddToCanvas() { }
        
        public virtual bool CollCheck(ReSizable reSizableBall) {
            return false;

        }
        //public virtual void CollCheck(ReSizablePolygon reSizablePolygons) { }
        //public virtual void CollCheck(ReSizableBall reSizableBall) { }

       // public virtual bool collCheckForPolygon( ReSizableBall reSizableBall) {}
        




    }
}
