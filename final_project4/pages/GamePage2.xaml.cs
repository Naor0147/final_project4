using final_project4.classes;
using final_project4.classes.Shapes;
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
        ReSizablePolygon pol2;

        private double angle = 0;

        public GamePage2()
        {
            this.InitializeComponent();
            gameCanvas = new GameCanvas(GameCanvas);
            SettingsClass.GameCanvas = gameCanvas;
            //pol1 =new ReSizablePolygon(new PhysicBody(x: 100, y: 100, vx: 100, vy: 0, ax: 0, ay: 0), 120, 100);
            pol2 = new ReSizablePolygon(new PhysicBody(x: 900, y: 200, vx: 100, vy: 400, ax: 0, ay: -0.098), 120, 100, 0);

            gameCanvas.AddToCanvas(pol2);
            border();
         

            LineCol lineCol5 = pol2.body.CreateVectorRepresentation();
            lineCol5.AddToCanvas(gameCanvas);

            Functions_add();
        }

        private void border()
        {
            ReSizablePolygon pol3 = new ReSizablePolygon(new PhysicBody(x: 0, y: 0, vx: 0, vy: 0, ax: 0, ay: 0), 1920, 1, 0.00001);
            ReSizablePolygon pol4 = new ReSizablePolygon(new PhysicBody(x: 0, y: 990, vx: 0, vy: 0, ax: 0, ay: 0), 1920, 1, 0.00001);
            gameCanvas.AddToCanvas(pol3);
            gameCanvas.AddToCanvas(pol4);
            ReSizablePolygon pol5 = new ReSizablePolygon(new PhysicBody(x: 0, y: 0, vx: 0, vy: 0, ax: 0, ay: 0), 1, 1000, 0);
            ReSizablePolygon pol6 = new ReSizablePolygon(new PhysicBody(x: 1910, y: 0, vx: 0, vy: 0, ax: 0, ay: 0), 1, 1000, 0);
            gameCanvas.AddToCanvas(pol5);
            gameCanvas.AddToCanvas(pol6);

            ReSizablePolygon pol8 = new ReSizablePolygon(new PhysicBody(x: 40, y: 500, vx: 0, vy: 0, ax: 0, ay: 0), 1, 1000, 280);
            gameCanvas.AddToCanvas(pol8);

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


            if (gameCanvas.checkCol())
            {
                Debug.Print("True ");
            }
            
            gameCanvas.MoveAll();
           
            
            fpstextblock.Text = $"vx: {pol2.body.vx:F2} \n vy: {pol2.body.vy:F2} \n ang {pol2.body.angle} \n {DebugClass.angleCollision}";
            
          
            gameCanvas.UpdateObjects() ;

        }
    }
}
