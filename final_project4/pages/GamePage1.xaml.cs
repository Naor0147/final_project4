﻿using final_project4.classes;
using final_project4.classes.Shapes;
using final_project4.classes.Shapes.Polygons;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace final_project4.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage1 : Page
    {
        private int _angle = 0;
        private GameHandler gameCanvas;
        private MyBall ball;

        public GamePage1()
        {
            this.InitializeComponent();
            gameCanvas = new GameHandler(Canvas,false);
            SettingsClass.GameCanvas = gameCanvas;

            ball = new MyBall(new PhysicBody(x: 150, y: 150, vx: 1, vy: 0, ax: 0, ay: 0, true), 50);
            gameCanvas.AddToCanvas(ball);
           
           
            gameCanvas.BuildBorders();
            gameObjects();
            //gameCanvas.UpdateSize(1, 1);
        }

        private void gameObjects()
        {
            
           
            gameCanvas.CreateWall(600, 550, 50, 1000);
            gameCanvas.CreateWall(600, 0, 50, 400);

            gameCanvas.CreateWall(700, 350, 50, 400, angle: 310);


            gameCanvas.CreateBasket(1700, 900, 100);
            gameCanvas.CreateCoin(1200, 200, 50);
            gameCanvas.CreateCoin(1100, 300, 50);
            gameCanvas.CreateCoin(1000, 400, 50);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SettingsClass.Change_To_Right_Screen_Ratio();
            
        }
    }
}