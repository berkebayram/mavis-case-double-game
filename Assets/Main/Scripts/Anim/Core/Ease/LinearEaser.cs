
using System;

namespace Anim.Ease
{
    public class LinearEaser : IAnimEaser
    {
        public float Ease(float progression)
        {
            return Math.Clamp(progression, 0, 1);
        }
    }
}
