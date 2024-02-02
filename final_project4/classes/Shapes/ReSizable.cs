namespace final_project4.classes.Shapes
{
    public enum CollisionType
    {
        False, Wall, Coin, Win
    }

    public abstract class ReSizable
    {
        public PhysicBody Body;
        public double Width, Height;

        protected ReSizable(PhysicBody body, double width = 0, double height = 0)
        {
            this.Body = body;
            this.Width = width;
            this.Height = height;
        }

        protected ReSizable()
        {
            this.Width = 0;
            this.Height = 0;
        }

        public virtual void UpdatePosAndSize()
        { }

        public virtual void AddToCanvas(GameCanvas gameCanvas)
        { }

        public virtual CollisionType CollCheck(ReSizable reSizableBall)
        {
            return CollisionType.False;
        }

        protected double GetAngleBetweenVectorAndLine(double Degree)
        {
            MyLine lineCol4 = this.Body.CreateVectorRepresentation();
            //lineCol4.AddToCanvas(SettingsClass.GameCanvas);
            double a = lineCol4.Degree;
            double b = Degree;//line2.Degree

            double ang = (2 * b - a);
            if (Body.vx < 0)
            {
                ang = 180 - (2 * b + a);
            }

            return ang;
        }

        //public virtual void CollCheck(MyPolygon reSizablePolygons) { }
        //public virtual void CollCheck(MyBall reSizableBall) { }

        // public virtual bool collCheckForPolygon( MyBall reSizableBall) {}
    }
}