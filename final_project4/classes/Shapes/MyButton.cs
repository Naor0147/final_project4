using final_project4.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace final_project4.classes.Shapes
{
    public class MyButton:ReSizable
    {
        public string Description { get; set; }
        public int FontSize { get; set; }

        public PhysicBody PhysicBody { get; set; }

        public Button Button { get; set; }

        public System.Type Page;
        public MyButton(string description, int fontSize, PhysicBody physicBody,System.Type page) : base(physicBody)
        {
            Description = description;
            FontSize = fontSize;
            PhysicBody = physicBody;
            Page = page;
           
            Button = new Button()
            {
                FontSize = fontSize,
                Content = description,
            };
            UpdatePosAndSize();
            Button.Click += Button_Click;
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(Page);
        }

        public override void UpdatePosAndSize()
        {
        
            if (PhysicBody == null) return;
            Button.FontSize = SettingsClass.Convert_To_Real(FontSize);
            Canvas.SetLeft(Button, PhysicBody.xReal - (Button.ActualWidth / 2));
            Canvas.SetTop(Button, PhysicBody.yReal)  ;
        }
        public override void AddToCanvas(GameCanvas canvas)
        {
            canvas.AddToCanvas(Button);
        }
    }

}
