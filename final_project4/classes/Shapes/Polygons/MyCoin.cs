﻿namespace final_project4.classes.Shapes.Polygons
{
    public class MyCoin : MyPolygon
    {
        public double CoinValue;
        public bool Collected = false;
        public MyCoin(PhysicBody physicBody, double size, double value = 10, string Id = "") : base(physicBody, size, size, 0, Id)
        {
            this.CoinValue = value;
            this.ChangeAppearance("Coin.png");

            this.Opacity = 1.0;
            this.TheBrush = null;
            ChangeLineType();
        }
        public override CollisionType CollCheck(ReSizable reSizable)
        {
            return base.CollCheck(reSizable);
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
