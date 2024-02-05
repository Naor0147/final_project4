using final_project4.classes;
using final_project4.classes.Shapes;
using final_project4.classes.Stats;
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
        public GameHandler gameCanvas;
        
        private MyBall ball;

      

        public GamePage2()
        {
            this.InitializeComponent();
            gameCanvas = new GameHandler(GameCanvas);
            SettingsClass.GameCanvas = gameCanvas;
            ball = new MyBall(new PhysicBody(x: 150, y: 150, vx: 10, vy: 400, ax: 0, ay: 0, true), 50);
            gameCanvas.AddToCanvas(ball);
            

            border();

          

           
        }

       

        private void border()
        {
            gameCanvas.CreateBasket(1700, 900, 100);
            gameCanvas.CreateCoin(1200, 200, 50);
            gameCanvas.CreateCoin(1100, 300, 50);
            gameCanvas.CreateCoin(1000, 400, 50);
         
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SettingsClass.Change_To_Right_Screen_Ratio();
            gameCanvas.UpdateObjects();
        }

     
     

       
    }
}