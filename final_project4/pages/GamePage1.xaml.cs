using final_project4.classes;
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
        private GameCanvas gameCanvas;

        public GamePage1()
        {
            this.InitializeComponent();
            gameCanvas = new GameCanvas(Canvas);

            CompositionTarget.Rendering += CompositionTarget_Rendering;

            //gameCanvas.UpdateSize(1, 1);
        }

        private void CompositionTarget_Rendering(object sender, object e)
        {
            _angle++;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SettingsClass.Change_To_Right_Screen_Ratio();
        }
    }
}