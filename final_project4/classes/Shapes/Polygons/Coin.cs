using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project4.classes.Shapes.Polygons
{
    public class Coin : MyPolygon
    {
        public Coin(PhysicBody physicBody, double size , string Id = "") : base(physicBody, size, size, 0, Id)
        {

            this.ChangeAperecnce("Coin.png");
          
            this.Opacity = 1.0;
            this.TheBrush = null;
            ChangeLineType();
        }

        private void ChangeLineType()
        {
            
            for (int i = 0; i < this.lines.Count; i++)
            {
                lines[i].LineType = LineType.Coin;
            }
        }


        // when i update the list the propreteys change 
        public override void CreateLineList()
        {
            base.CreateLineList();
            ChangeLineType();
            
        }
    }
}
