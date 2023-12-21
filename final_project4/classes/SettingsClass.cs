using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace final_project4.classes
{
    class SettingsClass
    {
        //const
        public const double IMAGINARY_SCREEN_HEIGHT = 1000;
        public const double IMAGINARY_SCREEN_WIDTH = 1920;

        public static double Window_VisibleBounds_Height = 1920;
        public static double Window_VisibleBounds_Width = 1000;
        /*
        What the differences between Imaginary canvas(the Calculation Canvas) and Real canvas(the Rendering Canvas) 
         
        to make the game work on any kind of screen I made concepts called (Imaginary Canvas)
        I Draw_line and work on the Imaginary Canvas is size doesn't change ,even if you change the window size ,
        then i after i Draw_line on the imaginary canvas i convert the object position and size to the size of the 
        "Real Canvas" (the canvas you see when you play the game), and every time you resize the screen the objects 
        update their size accordingly to the screen size 
         
         */



        public static double current_FPS = 144;//sets the value really high so the object doesn't move until the fps has been set

        public static double Convert_To_Real(double value)
        {
            /*var window = ApplicationView.GetForCurrentView();


            Debug.WriteLine($"  window visible bounds {window.VisibleBounds.Height} ");*/

            return value * (Window_VisibleBounds_Height / IMAGINARY_SCREEN_HEIGHT);
        }
        public static double ConvertAngleRadian(double angle) => angle* 0.0174532925;
        public static double ConvertRadianDegree(double radian) => (180 / Math.PI) * radian;



        //Every time the screen size change the function make sure the ratio of the width and height stay the same 
        public static void Change_To_Right_Screen_Ratio()
        {
            var window = ApplicationView.GetForCurrentView();

            //Imaginary screen ratio {width/height}
            double ratio = IMAGINARY_SCREEN_WIDTH / IMAGINARY_SCREEN_HEIGHT;


            //Update the engine current height and width 
            Window_VisibleBounds_Height = window.VisibleBounds.Height;
            Window_VisibleBounds_Width = window.VisibleBounds.Width;

            //check if the current ratio is the same as the desirable ratio 

            if (Window_VisibleBounds_Width / Window_VisibleBounds_Height != ratio)
            {
                double newWidth = window.VisibleBounds.Height * ratio;

                window.TryResizeView(new Windows.Foundation.Size(newWidth, window.VisibleBounds.Height));
            }
        }


        public static bool isBetween(double v1, double middle, double v2)
        {

            if (v1 > v2)
            {
                double temp = v2;
                v2 = v1;
                v1 = temp;
            }
            return v1 < middle && middle < v2;
        }

    }
}
