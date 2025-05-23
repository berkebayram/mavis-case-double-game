using UnityEngine;

namespace Anim.Calculator
{
    public class Vector3Lerper : IAnimCalculator<Vector3>
    {
        public Vector3 Start { get; set; }
        public Vector3 End { get; set; }

        public Vector3Lerper(Vector3 start, Vector3 end)
        {
            Start = start;
            End = end;
        }

        public Vector3 Calculate(float progression)
        {
            return Vector3.Lerp(Start, End, progression);
        }
    }
}
