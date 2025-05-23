
using UnityEngine;

namespace Anim.Calculator
{
    public class QuadraticBezierCalculator2D : IAnimCalculator<Vector3>
    {
        public Vector3 Start { get; set; }
        public Vector3 End { get; set; }
        public Vector3 MiddlePoint { get; private set; }
        public QuadraticBezierCalculator2D(Vector2 start, Vector2 middle, Vector2 end)
        {
            Start = start;
            End = end;
            MiddlePoint = middle;
        }

        public Vector3 Calculate(float progression)
        {
            var x = (1f - progression);
            return
                x * x * Start
                + 2 * x * progression * MiddlePoint
                + progression * progression * End;
        }
    }
}
