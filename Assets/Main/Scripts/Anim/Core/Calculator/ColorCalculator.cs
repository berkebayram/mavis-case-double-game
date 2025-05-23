
using UnityEngine;

namespace Anim.Calculator
{
    public class ColorLerper : IAnimCalculator<Color>
    {
        public Color Start { get; set; }
        public Color End { get; set; }

        public ColorLerper(Color start, Color end)
        {
            Start = start;
            End = end;
        }

        public Color Calculate(float progression)
        {
            return Color.Lerp(Start, End, progression);
        }
    }
}
