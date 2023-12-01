using final_project4.classes;
using final_project4.classes.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace final_project4.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage2 : Page
    {
        ReSizablePolygon reSizablePolygon;
        public int frameCount = 0;
        public DispatcherTimer fpsTimer;
        public GameCanvas gameCanvas;
        ReSizablePolygon pol1;

        private double angle = 0;

        public GamePage2()
        {
            this.InitializeComponent();
            gameCanvas = new GameCanvas(GameCanvas);
            
            pol1 =new ReSizablePolygon(new PhysicBody(x: 100, y: 100, vx: 100, vy: 0, ax: 0, ay: 0), 120, 100);
            ReSizablePolygon pol2 = new ReSizablePolygon(new PhysicBody(x: 900, y: 100, vx: -100, vy: 0, ax: 0, ay: 0), 120, 100);
            gameCanvas.AddToCanvas(pol1);
            gameCanvas.AddToCanvas(pol2);


            ReSizableBall reSizableBall = new ReSizableBall(new PhysicBody(x: 100, y: 100, vx: 90, vy: 0, ax: 0, ay: 0), 400);
           
            gameCanvas.AddToCanvas(reSizableBall);
            gameCanvas.AddToCanvas(reSizableBall.rect);


            for (int i = 0; i < 500; i++)
            {
                ReSizablePolygon pol3 = new ReSizablePolygon(new PhysicBody(x: i*10, y: 1000-i*10, vx: -100, vy: 0, ax: 0, ay: 0), 50, 50);
                gameCanvas.AddToCanvas(pol3);
            }


            Functions_add();
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
            fpstextblock.Text = $"FPS: {frameCount:F2}";

            classes.SettingsClass.current_FPS = frameCount;
            // Reset counters
            frameCount = 0;
        }

        
        private void CompositionTarget_Rendering(object sender, object e)
        {
            frameCount++;
          //  gameCanvas.MoveAll();
           
            if (gameCanvas.checkCol())
            {
                Debug.Print("True ");
            }
            angle+=0.5;
            pol1.angle = angle;
            //pol1.UpdatePosAndSize();
            fpstextblock.Text = $"frame: {SettingsClass.current_FPS:F4}";


        }
    }
}
