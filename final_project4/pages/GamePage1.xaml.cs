using final_project4.classes;
using final_project4.classes.Shapes;
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
            gameCanvas = new GameHandler(Canvas);
            ball = new MyBall(new PhysicBody(x: 150, y: 150, vx: 10, vy: 400, ax: 0, ay: 0, true), 50);
            gameCanvas.AddToCanvas(ball);

            //gameCanvas.UpdateSize(1, 1);
        }

       

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SettingsClass.Change_To_Right_Screen_Ratio();
            
        }
    }
}