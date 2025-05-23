

using System;

namespace Anim.Ease
{
    public class OutQuadEaser : IAnimEaser
    {
        public float Ease(float progression)
        {
            progression = Math.Clamp(progression, 0, 1);

            return 1f - ((1f - progression) * (1f - progression));
        }
    }
}
