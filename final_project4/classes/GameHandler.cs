using final_project4.classes.Shapes.Polygons;
using final_project4.classes.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using final_project4.classes.Stats;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace final_project4.classes
{
    public class GameHandler : GameCanvas
    {

        //fps
        public int frameCount = 0;
        //timer
        public DispatcherTimer fpsTimer;


        //Text
        public MyText ScoreText;

        public MyText TimeText;
        public MyText TimeClickedText;
        public MyText OnGroundText;


        //score
        public LevelStats LevelStats { get; set; } = new LevelStats();

        public int TimeClicked = 0;



        public GameHandler(Canvas MainCanvas,bool AddBorders=true) : base(MainCanvas)
        {
           
            MainCanvas.PointerPressed += MainCanvas_PointerPressed;
            Functions_add();
            if (AddBorders)
            {
                BuildBorders();
            }
        }
        private void Functions_add()
        {
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            fpsTimer = new DispatcherTimer();
            fpsTimer.Interval = TimeSpan.FromSeconds(1);
            fpsTimer.Tick += FpsTimer_Tick;
            fpsTimer.Start();
            
        }

        private void FpsTimer_Tick(object sender, object e)
        {
            if (LevelStats.Won) return;
            
            LevelStats.TimePassed++;
            TimeText.Variable = LevelStats.TimePassed + "";
            classes.SettingsClass.current_FPS = (uint)frameCount;
            frameCount = 0;
        }

        private void CompositionTarget_Rendering(object sender, object e)
        {
            frameCount++;

            DebugClass.FrameCounter += 1;

            Functions();
        }

        public override void Functions()
        {
            base.Functions();
            CheckCol();
        }


        private void MainCanvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            // Get the position of the pointer relative to the MainCanvas
            Point position = e.GetCurrentPoint(MainCanvas).Position;

            // Display the coordinates and show where the player clicked on the screen
            position = DisplayCoordinatesInConsole(position);

            if (MyBall == null) return;
            
            Point point = new Point(SettingsClass.Convert_To_Img(position.X), SettingsClass.Convert_To_Img(position.Y));


            /*debug
            //Point ballPoint = new Point(MyBall.Body.x + (MyBall.Width / 2), MyBall.Body.y + (MyBall.Height / 2));
            // MyLine line = new MyLine(point, ballPoint);
            //line.AddToCanvas(gameCanvas);*/

            //Calculate the velocity of the ball
            MyBall.ClickOnTheScreen(point);

            //stats updater 
            ClickStatsUpdate();
        }

        //display where the player clicked (on screen postion and realtive screen)
        private static Point DisplayCoordinatesInConsole(Point position)
        {
            System.Diagnostics.Debug.WriteLine($"X: {position.X}, Y: {position.Y}");
            System.Diagnostics.Debug.WriteLine($"X: {SettingsClass.Convert_To_Img(position.X)}, Y: {SettingsClass.Convert_To_Img(position.Y)}");
            return position;
        }

        //updates how many time the player clicked on the screen 
        private void ClickStatsUpdate()
        {
            if (LevelStats.Won) return;
           
            LevelStats.TimeClicked++;
            TimeClickedText.Variable = LevelStats.TimeClicked + "";
        }



        /// <summary>
        /// This how i check collision
        /// </summary>
        public void CheckCol()
        {
            
            bool temp = false;

            for (int j = 0; j < ReList.Count; j++)
            {
                //check grvaity 
                 temp = MyBall.OnGround(ReList[j] as MyPolygon);
                if (temp)
                {
                    break;
                }

                //HandleCollisonPerTwoItems(j);
                MyBall.NewCollision(ReList[j] as MyPolygon);

            }

            SettingsClass.QueueInArr(MyBall.Body.OnGround,temp);//update the array
           
            OnGround = MyBall.Body.IsReallyOnGround() ;
            //if (OnGround)
            //{
            //    MyBall.Body.y -= 1;
            //}
            OnGroundText.Variable = OnGround+ "";
            MyBall.Body.HaveGravity= !OnGround;
                
            
        }
        public bool OnGround=false;
        protected void HandleCollisonPerTwoItems( int j)
        {
            if (MyBall==null|| ReList[j]==MyBall || !MyBall.Body.HaveGravity) return;
            switch (MyBall.CollCheckForPolygon(ReList[j] as MyPolygon))
            {
                case CollisionType.Coin:
                    {
                        CoinHandler(j);
                        break;
                    }
                case CollisionType.Win:
                    {
                        WinHandler();
                        break;
                    }
                case CollisionType.Wall:

                    break;
                case CollisionType.False:
                    break;
               
            }
        }


        private void CoinHandler(int j)
        {
            ReList[j].Height = 0;
            if (!(ReList[j] is MyCoin)) return;
            
            MyCoin coin = ReList[j] as MyCoin;

            if (coin.Collected) return;

            LevelStats.AmountOfCoinsCollected++;
            LevelStats.CoinsTotal += coin.CoinValue;
            ScoreText.Variable = LevelStats.CoinsTotal + "";

            removeObject(ReList[j]);
            coin.Collected = true;
        }

        private void WinHandler()
        {
            //SettingsClass.current_FPS = 0;//stop the game
            MyBall.Body.x = 10000;

            LevelStats.Won = true;
            WinScreen();
        }

        private void WinScreen()
        {
            CreateWall(0, 0, 1920, 1000);
            CreateText(SettingsClass.IMAGINARY_SCREEN_WIDTH / 2, SettingsClass.IMAGINARY_SCREEN_HEIGHT / 3-100, 200, "You won");

            MoveTextObject(ScoreText, SettingsClass.IMAGINARY_SCREEN_WIDTH / 2, SettingsClass.IMAGINARY_SCREEN_HEIGHT / 3 + 200, 5);

            MoveTextObject(TimeText, SettingsClass.IMAGINARY_SCREEN_WIDTH / 2, SettingsClass.IMAGINARY_SCREEN_HEIGHT / 3 + 300, 6);

            MoveTextObject(TimeClickedText, SettingsClass.IMAGINARY_SCREEN_WIDTH / 2, SettingsClass.IMAGINARY_SCREEN_HEIGHT / 3 + 400, 7);

            MyButton myButton = new MyButton("Go back", 40, new PhysicBody(SettingsClass.IMAGINARY_SCREEN_WIDTH / 2, SettingsClass.IMAGINARY_SCREEN_HEIGHT / 3 + 500),typeof(MainPage));
            AddToCanvas(myButton);
           
        }

     

        private void MoveTextObject(MyText myText, double x,double y,int z)
        {
            myText.PhysicBody.TransformPosition(new Point(x, y));
            Canvas.SetZIndex(myText.TextBlock, z);
        }

       
       







        //builder
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

           
            CreateStats();
        }

        public void CreateStats()
        {
            ScoreText = CreateText(SettingsClass.IMAGINARY_SCREEN_WIDTH / 2 - 100, 0, 40, "Money collected = ", (LevelStats.CoinsTotal + ""));

            TimeClickedText = CreateText(SettingsClass.IMAGINARY_SCREEN_WIDTH / 2 - 100, 50, 40, "Clicked ", LevelStats.TimeClicked + "", " time ");

            TimeText = CreateText(SettingsClass.IMAGINARY_SCREEN_WIDTH / 4 - 100, 0, 40, "Time passed= ", LevelStats.TimePassed + "");
            OnGroundText = CreateText(SettingsClass.IMAGINARY_SCREEN_WIDTH / 4 * 3 - 100, 0, 40, "On ground ", false + "");
        }
    }
}
