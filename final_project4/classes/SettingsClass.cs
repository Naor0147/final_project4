using System;
using Windows.UI.ViewManagement;

namespace final_project4.classes
{
    struct SettingsClass
    {
        //const
        public const double IMAGINARY_SCREEN_HEIGHT = 1000;

        public const double IMAGINARY_SCREEN_WIDTH = 1920;

        //let say the screen size in 19.2 meter on 10 metter

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
        public static GameHandler GameCanvas;

        public static double current_FPS = 144;//sets the value really high so the object doesn't move until the fps has been set

        public static double Convert_To_Img(double value)
        {
            /*var window = ApplicationView.GetForCurrentView();

            Debug.WriteLine($"  window visible bounds {window.VisibleBounds.Height} ");*/

            return value * (IMAGINARY_SCREEN_HEIGHT / Window_VisibleBounds_Height);
        }

        public static double Convert_To_Real(double value)
        {
            /*var window = ApplicationView.GetForCurrentView();

            Debug.WriteLine($"  window visible bounds {window.VisibleBounds.Height} ");*/

            return value * (Window_VisibleBounds_Height / IMAGINARY_SCREEN_HEIGHT);
        }

        public static double ConvertAngleRadian(double angle) => angle * 0.0174532925;

        public static double ConvertRadianDegree(double radian) => (180 / Math.PI) * radian;

        public static double PythagoreanTheorem(double a, double b) => (Math.Sqrt(a * a + b * b));

        public static double DistanceBetweenTwoPoints(double x1, double y1, double x2, double y2) => Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

        public static double DistanceBetweenTwoPoints(Windows.Foundation.Point point1, Windows.Foundation.Point point2) => Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));

        //Every time the screen size change the Function make sure the ratio of the width and height stay the same
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