using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project4.classes.Shapes
{
    public class MyBasket : MyPolygon
    {
        public static double ImgRatio = 454.0 / 281.0;
        public MyBasket(PhysicBody physicBody, double height) : base(physicBody, height* ImgRatio, height, 0, "basket")
        {
            this.ChangeAperecnce("basket2.png");
            //454 on 281
            this.Opacity = 1.0;
            this.TheBrush = null;
            ChangeLineType();
        }

        private void ChangeLineType()
        {
            lines[3].LineType = LineType.Win;
            /*
            for (int i = 0; i < this.lines.Count; i++)
            {
                lines[i].LineType = LineType.Win;
            }*/
        }
        private void changeFriction()
        {
            for (int i = 0; i < this.lines.Count; i++)
            {
                lines[i].Friction = 1;
            }
        }
        public override void CreateLineList()
        {
            base.CreateLineList();
            ChangeLineType();
            changeFriction();
        }
    }
}
