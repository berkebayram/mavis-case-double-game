
using System;

namespace Anim.Ease
{
    public class InQuadEaser : IAnimEaser
    {
        public float Ease(float progression)
        {
            progression = Math.Clamp(progression, 0, 1);
            return progression * progression;
        }
    }
}
