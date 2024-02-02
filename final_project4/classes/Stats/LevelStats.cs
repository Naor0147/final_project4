using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace final_project4.classes.Stats
{
    public class LevelStats
    {
        public double AmountOfCoinsCollected { get; set; } = 0;
        public double CoinsTotal { get; set; } = 0;
        public double TimePassed { get; set; } = 0;
        public int TimeClicked { get; set; } = 0;
        public bool Won { get; set; } = false;


        public LevelStats() { }
    }
}
