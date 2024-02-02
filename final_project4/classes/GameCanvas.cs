using final_project4.classes.Shapes;
using final_project4.classes.Shapes.Polygons;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace final_project4.classes
{
    public class GameCanvas
    {
        public Canvas MainCanvas { get; set; }

        public List<ReSizable> ReList { get; set; }

        public List<MyLine> ReLineList { get; set; }

        public List<MyText> MyTextList { get; set; }

        //Text
        public MyText ScoreText;

        public MyText TimeText;
        public MyText TimeClickedText;

        //score
        public double score = 0;

        public double timer = 0;
        public int TimeClicked = 0;

        public GameCanvas(Canvas canvas)
        {
            MainCanvas = canvas;
            ReList = new List<ReSizable>();

            ReLineList = new List<MyLine>();
            MyTextList = new List<MyText>();
        }

        public void AddToCanvas<T>(T shape)
        {
            if (shape is ReSizable)
            {
                ReList.Add(shape as ReSizable);
            }

            switch (shape)
            {
                case MyPolygon pol:

                    MainCanvas.Children.Add(pol.realPolygon);

                    break;

                case MyBall ball:

                    MainCanvas.Children.Add(ball.BallEllipse);

                    break;

                case MyLine lineCol:
                    MainCanvas.Children.Add(lineCol.line);
                    ReLineList.Add(lineCol);
                    break;

                case MyText text:
                    MainCanvas.Children.Add(text.TextBlock);
                    MyTextList.Add(text);
                    break;
            }
        }

        //public update
        public void UpdateObjects()
        {
            if (ReList.Count == 0) return;
            foreach (var item in ReList)
            {
                if (item == null) continue;

                item.UpdatePosAndSize();
            }
            foreach (var item in ReLineList)
            {
                if (item == null) continue;

                item.UpdateLineSize();
            }

            foreach (var item in MyTextList)
            {
                if (item == null) continue;

                item.UpdatePositionAndSize();
            }
        }

        public void MoveAll()
        {
            //foreach (ReSizable polygon in ReList)
            foreach (ReSizable polygon in ReList.Where(polygon => polygon != null))
            {
                if (polygon.Body != null && polygon.Body.Movable)
                {
                    polygon.Body.Move(SettingsClass.current_FPS);
                }
                polygon.UpdatePosAndSize();
            }
            foreach (MyText text in MyTextList.Where(text => text != null))
            {
                if (text.PhysicBody != null && text.PhysicBody.Movable)
                {
                    text.PhysicBody.Move(SettingsClass.current_FPS);
                }
                text.UpdatePositionAndSize();
            }
        }

        public void checkCol()
        {
            for (int i = 0; i < ReList.Count - 1; i++)
            {
                for (int j = i + 1; j < ReList.Count; j++)
                {
                    HandleCollisonPerTwoItems(i, j);
                }
            }
        }

        private void HandleCollisonPerTwoItems(int i, int j)
        {
            if (!IsValidCollCheck(i)) return;

            switch (CollCheckTwoObjects(ReList[i], ReList[j]))
            {
                case CollisionType.Coin:
                    {
                        ReList[j].Height = 0;
                        if (ReList[j] is MyCoin)
                        {
                            MyCoin coin = ReList[j] as MyCoin;
                            if (!coin.Collected)
                            {
                                score += coin.CoinValue;
                                ScoreText.Variable = score + "";
                                ScoreText.UpdatePositionAndSize();
                                coin.Collected = true;
                            }
                        }
                        break;
                    }
            }
        }

        private bool IsValidCollCheck(int i)
        {
            //return ReList[i] != null && ReList[i].body != null && ReList[i].body.movable == true;
            return (ReList[i] is MyBall);
        }

        private static CollisionType CollCheckTwoObjects(ReSizable re1, ReSizable re2)
        {
            switch (re1)
            {
                case MyPolygon reSizablePolygon:
                    return reSizablePolygon.CollCheck(re2);

                case MyBall reSizableBall:
                    return reSizableBall.CollCheck(re2);
            }

            return CollisionType.False;
        }

        public MyWall CreateWall(double x, double y, double width, double height, WallStyle wallStyle = WallStyle.Regular, double angle = 0)
        {
            MyWall wall = new MyWall(new PhysicBody(x, y), width, height, wallStyle, angle);
            AddToCanvas(wall);
            return wall;
        }

        public MyCoin CreateCoin(double x, double y, double size, double value = 10)
        {
            MyCoin coin = new MyCoin(new PhysicBody(x, y), size, value);
            AddToCanvas(coin);
            return coin;
        }

        public MyBasket CreateBasket(double x, double y, double size)
        {
            MyBasket myBasket = new MyBasket(new PhysicBody(x, y), size);
            AddToCanvas(myBasket);
            return myBasket;
        }

        public MyText CreateText(double x, double y, double font, string text1 = "", string var = "", string text2 = "")
        {
            MyText Score = new MyText(text1, var, text2, new PhysicBody(x, y), font);
            AddToCanvas(Score);
            return Score;
        }

        public void BuildBorders()
        {
            CreateWall(0, 0, 1920, 100);

            //top and bottom
            CreateWall(0, 100, 1920, 50, WallStyle.Celling);
            CreateWall(0, 950, 1920, 50, WallStyle.Celling);
            //walls
            CreateWall(0, 150, 50, 840, WallStyle.SideWall);
            CreateWall(1870, 150, 50, 840, WallStyle.SideWall);

            //wall in angle
            CreateWall(0, 550, 50, 1000, angle: 280);

            ScoreText = CreateText(SettingsClass.IMAGINARY_SCREEN_WIDTH / 2 - 100, 0, 40, "ScoreText = ", (score + ""));

            TimeClickedText = CreateText(SettingsClass.IMAGINARY_SCREEN_WIDTH / 2 - 100, 50, 40, "Clicked ", TimeClicked + "", " time ");
        }

        public void Functions()
        {
            checkCol();

            MoveAll();

            UpdateObjects();
        }
    }
}