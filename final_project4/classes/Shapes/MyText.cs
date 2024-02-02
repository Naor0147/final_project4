using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace final_project4.classes.Shapes
{
    public class MyText 
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Variable {  get; set; }
        public double FontSize { get; set; }
        public PhysicBody PhysicBody { get; set; }
        public TextBlock TextBlock {  get; set; }
        public MyText(string text, PhysicBody physicBody, double FontSize=24)
        {
            this.Text1 = text;
            this.Text2 = "";
            this.PhysicBody = physicBody;
            this.FontSize = FontSize;
            TextBlock = new TextBlock
            {
                Text = Text1 ,
                FontSize = FontSize,
            };
            UpdatePositionAndSize();

        }

        public MyText(string text1,string variable, string text2, PhysicBody physicBody, double FontSize = 24)
        {
            this.Text1 = text1;
            this.Text2 = text2;
            this.Variable = variable; 
            this.PhysicBody = physicBody;
            this.FontSize = FontSize;
            TextBlock = new TextBlock
            {
                Text = text1 + variable+text2 ,
                FontSize = FontSize,
            };
            UpdatePositionAndSize();

        }

      

        public void UpdatePositionAndSize()
        {
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
