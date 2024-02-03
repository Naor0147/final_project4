
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace final_project4.classes.Shapes
{
    public class MyText:ReSizable
    {
        private string _text1;
        private string _text2;

        public string Text1
        {
            get
            {
                return _text1;
            }
            set
            {
                _text1 = value;
                UpdatePosAndSize();
            }
        }
        public string Text2
        {
            get
            {
                return _text2;
            }
            set
            {
                _text2 = value;
                UpdatePosAndSize();
            }
        }
        private string _variable;
        public string Variable { 
            get
            { 
                return _variable;
            }
            set
            {
                _variable = value;
                UpdatePosAndSize();
            }
        }
        public double FontSize { get; set; }
        public PhysicBody PhysicBody { get; set; }
        public TextBlock TextBlock { get; set; }

        public MyText(string text, PhysicBody physicBody, double FontSize = 24):base(physicBody)
        {
            this.Text1 = text;
            this.Text2 = "";
            this.PhysicBody = physicBody;
            this.FontSize = FontSize;
            TextBlock = new TextBlock
            {
                Text = Text1,
                FontSize = FontSize,
            };
            UpdatePosAndSize();
        }

        public MyText(string text1, string variable, string text2, PhysicBody physicBody, double FontSize = 24) : base(physicBody)
        {
            this.Text1 = text1;
            this.Text2 = text2;
            this.Variable = variable;
            this.PhysicBody = physicBody;
            this.FontSize = FontSize;
            TextBlock = new TextBlock
            {
                Text = text1 + variable + text2,
                FontSize = FontSize,
            };
            UpdatePosAndSize();
        }

        public override void UpdatePosAndSize()
        {
           
            if (PhysicBody==null) return;
            TextBlock.FontSize = SettingsClass.Convert_To_Real(FontSize);
            TextBlock.Text = this.Text1 + this.Variable + this.Text2;

            Canvas.SetLeft(TextBlock , PhysicBody.xReal - (TextBlock.ActualWidth / 2));
            Canvas.SetTop(TextBlock, PhysicBody.yReal );
           
        }

        public override void AddToCanvas(GameCanvas canvas)
        {
            canvas.AddToCanvas(TextBlock);
        }

        


    }
}