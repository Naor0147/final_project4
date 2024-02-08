namespace final_project4.classes
{
    public struct PointCol
    {
        public bool Collision;
        public double x;
        public double y;

        public double angle;

        public PointCol(double x, double y)
        {
            Collision = true;
            this.x = x;
            this.y = y;
            this.angle = 0;
        }

        public PointCol(double x, double y, double angle)
        {
            Collision = true;
            this.x = x;
            this.y = y;
            this.angle = angle;
        }

        public PointCol(double x, double y, bool collation)
        {
            this.Collision = collation;
            this.x = x;
            this.y = y;
            this.angle = 0;
        }

        public PointCol(double x, double y, double angle, bool collation)
        {
            this.Collision = collation;
            this.x = x;
            this.y = y;
            this.angle = angle;
        }

        public PointCol(double x = 0)
        {
            Collision = false;
            this.x = 0;
            this.y = 0;
            this.angle = 0;
        }
    }
}