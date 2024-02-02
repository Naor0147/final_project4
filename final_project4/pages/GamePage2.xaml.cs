using final_project4.classes;
using final_project4.classes.Shapes;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Point = Windows.Foundation.Point;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace final_project4.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage2 : Page
    {
      
        public int frameCount = 0;
        public DispatcherTimer fpsTimer;
        public GameCanvas gameCanvas;
        
        private MyBall ball;
        

        public GamePage2()
        {
            this.InitializeComponent();
            gameCanvas = new GameCanvas(GameCanvas);

            ball = new MyBall(new PhysicBody(x: 150, y: 150, vx: 10, vy: 400, ax: 0, ay: 0, true), 50);
            gameCanvas.AddToCanvas(ball);

            gameCanvas.BuildBorders();
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
            System.Diagnostics.Debug.WriteLine($"X: {SettingsClass.Convert_To_Img(position.X)}, Y: {SettingsClass.Convert_To_Img(position.Y)}");
            Point point = new Point(SettingsClass.Convert_To_Img(position.X), SettingsClass.Convert_To_Img(position.Y));
            Point ballPoint = new Point(ball.Body.x + (ball.Width / 2), ball.Body.y + (ball.Height / 2));
            // MyLine line = new MyLine(point, ballPoint);
            //line.AddToCanvas(gameCanvas);
            ball.ClickOnTheScreen(point);
        

        }

        private void border()
        {
            gameCanvas.CreateBasket(1700, 900, 100);
            //MyPolygon pol9 = new MyPolygon(new PhysicBody(x: 400, y: 700, vx: 0, vy: 0, ax: 0, ay: 0), 400, 50, 20);
            //gameCanvas.AddToCanvas(pol9);
            gameCanvas.CreateCoin(1200, 200, 50);
            gameCanvas.CreateCoin(1100, 300, 50);
            gameCanvas.CreateCoin(1000, 400, 50);
         
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

            gameCanvas.Functions();
        }
    }
}