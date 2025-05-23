using UnityEngine;

namespace Anim.Calculator
{
    public class FloatLerper : IAnimCalculator<float>
    {
        public float Start { get;set; }

        public float End { get;set; }

        public FloatLerper(float start, float end)
        {
            Start = start;
            End = end;
        }

        public float Calculate(float progression)
        {
            return Mathf.Lerp(Start, End, progression);
        }
    }
}
