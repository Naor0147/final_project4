using final_project4.classes;
using final_project4.classes.Shapes;
using final_project4.classes.Shapes.Polygons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Point = Windows.Foundation.Point;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace final_project4.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage2 : Page
    {
        MyPolygon reSizablePolygon;
        public int frameCount = 0;
        public DispatcherTimer fpsTimer;
        public GameCanvas gameCanvas;
        MyPolygon pol2;
        MyBall ball;
        private double angle = 0;

        public GamePage2()
        {
            this.InitializeComponent();
            gameCanvas = new GameCanvas(GameCanvas);
            SettingsClass.GameCanvas = gameCanvas;
            
           
           ball = new MyBall(new PhysicBody(x: 100, y: 100, vx:10, vy: 400, ax: 0, ay: 0, true), 50);
            gameCanvas.AddToCanvas(ball);


            //            mainCanvas.PointerPressed += OnCanvasClick;


            border();
         
            
            Functions_add();
            GameCanvas.PointerPressed += GameCanvas_PointerPressed;
        }

        private void GameCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // Get the position of the pointer relative to the canvas
            Point position = e.GetCurrentPoint(GameCanvas).Position;

            // Display the coordinates
            System.Diagnostics.Debug.WriteLine($"X: {position.X}, Y: {position.Y}");
            System.Diagnostics.Debug.WriteLine($"X: {SettingsClass.Convert_To_Img( position.X)}, Y: {SettingsClass.Convert_To_Img(position.Y)}");
            Point point = new Point(SettingsClass.Convert_To_Img(position.X), SettingsClass.Convert_To_Img(position.Y));
            Point ballPoint= new Point( ball.body.x+(ball.width/2),ball.body.y + (ball.height/2) );
           // MyLine line = new MyLine(point, ballPoint);
            //line.AddToCanvas(gameCanvas);
            
            fpstextblock.Text = "\n \n"+ball.ClickOnTheScreen(point);

            // You can do more with the click coordinates here
        }

        private void border()
        {
            //top and botoom 
            CreateWall(0,0, 1920, 50);
            CreateWall(0, 990, 1920, 50);
            //wallls
            CreateWall(0, 0, 50, 1000);
            CreateWall(1870, 0, 50, 1000);
            
            //wall in angle 
           CreateWall(0, 550, 50, 1000, 280);
         

            MyBasket basket = new MyBasket(new PhysicBody(1700, 900), 100);
            gameCanvas.AddToCanvas(basket);
            //MyPolygon pol9 = new MyPolygon(new PhysicBody(x: 400, y: 700, vx: 0, vy: 0, ax: 0, ay: 0), 400, 50, 20);
            //gameCanvas.AddToCanvas(pol9);
            Coin coin = new Coin(new PhysicBody(1200, 400), 100);
            gameCanvas.AddToCanvas(coin);

        }

        public void CreateWall(double x, double y, double width, double height,double angle = 0)
        {
            MyWall wall = new MyWall(x, y, width, height, angle);
            gameCanvas.AddToCanvas(wall);
        }


        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SettingsClass.Change_To_Right_Screen_Ratio();
            gameCanvas.UpdateObjects();
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
           // fpstextblock.Text = $"FPS: {frameCount:F2}";

            classes.SettingsClass.current_FPS = frameCount;
            // Reset counters
            frameCount = 0;
        }

        
        private void CompositionTarget_Rendering(object sender, object e)
        {
            frameCount++;

            DebugClass.FrameCounter += 1;


            gameCanvas.checkCol();
          
            
            gameCanvas.MoveAll();
           
            
           // fpstextblock.Text = $"vx: {ball.body.vx:F2} \n vy: {ball.body.vy:F2} \n ang {ball.body.angle} \n {DebugClass.angleCollision}";
            
          
            gameCanvas.UpdateObjects() ;

        }
    }
}
