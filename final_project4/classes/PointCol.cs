using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project4.classes
{
    public struct PointCol
    {
        bool collation;
        public double x;
        public double y;

        public double angle;

        public PointCol(double x, double y, double angle)
        {
            collation = true;
            this.x = x;
            this.y = y;
            this.angle = angle;
        }
        public PointCol(double x = 0)
        {
            collation = false;
            this.x = 0;
            this.y = 0;
            this.angle =0;
        }
        
    } 
}
