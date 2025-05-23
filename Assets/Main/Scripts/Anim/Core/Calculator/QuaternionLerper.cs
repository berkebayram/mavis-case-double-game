using UnityEngine;

namespace Anim.Calculator
{
    public class QuaternionLerper : IAnimCalculator<Quaternion>
    {
        public Quaternion Start { get; set; }
        public Quaternion End { get; set; }

        public QuaternionLerper(Quaternion start, Quaternion end)
        {
            Start = start;
            End = end;
        }

        public Quaternion Calculate(float progression)
        {
            return Quaternion.Lerp(Start, End, progression);
        }
    }
}
