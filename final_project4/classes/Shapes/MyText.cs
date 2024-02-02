using Windows.UI.Xaml.Controls;

namespace final_project4.classes.Shapes
{
    public class MyText
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
                UpdatePositionAndSize();
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
                UpdatePositionAndSize();
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
                UpdatePositionAndSize();
            }
        }
        public double FontSize { get; set; }
        public PhysicBody PhysicBody { get; set; }
        public TextBlock TextBlock { get; set; }

        public MyText(string text, PhysicBody physicBody, double FontSize = 24)
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
            UpdatePositionAndSize();
        }

        public MyText(string text1, string variable, string text2, PhysicBody physicBody, double FontSize = 24)
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
            UpdatePositionAndSize();
        }

        public void UpdatePositionAndSize()
        {
            if (PhysicBody==null) return;
            
            Canvas.SetLeft(TextBlock, PhysicBody.xReal);
            Canvas.SetTop(TextBlock, PhysicBody.yReal);
            TextBlock.FontSize = SettingsClass.Convert_To_Real(FontSize);
            TextBlock.Text = this.Text1 + this.Variable + this.Text2;
        }

        public void AddToCanvas(GameCanvas canvas)
        {
            canvas.AddToCanvas(TextBlock);
        }
    }
}