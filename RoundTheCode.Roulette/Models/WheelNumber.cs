using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTheCode.Roulette.Models
{
    public class WheelNumber
    {
        public int Number { get; }

        public WheelNumberColourEnum Colour { get; }

        public bool Selected { get; set; }

        public WheelNumber(int number, WheelNumberColourEnum colour)
        {
            Number = number;
            Colour = colour;
        }
    }
}
