using final_project4.classes;
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
            gameCanvas.CreateStats();
            ball = new MyBall(new PhysicBody(x: 150, y: 150, vx: 10, vy: 400, ax: 0, ay: 0, true), 50);
            gameCanvas.AddToCanvas(ball);
            MyWall mywall = new MyWall(new PhysicBody(0, 800), 1000, 100,WallStyle.Regular);
            int id = 1;
            foreach(var line in mywall.lines)
            {
                line.id=id; 
                id++;
            }
            gameCanvas.AddToCanvas(mywall);
            //gameCanvas.UpdateSize(1, 1);
        }

       

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SettingsClass.Change_To_Right_Screen_Ratio();
            
        }
    }
}