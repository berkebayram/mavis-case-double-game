using System.Collections;
using Anim.Ease;
using Anim.Binder;
using Anim.Calculator;
using System;

namespace Anim
{
    public interface IAnim<V> : IPlayable
    {
        public IAnimEaser Easer { get; }
        public IAnimBinder<V> Binder { get; }
        public IAnimCalculator<V> Calculator { get; }

        public IEnumerator GetCoroutine(Func<float> deltaTimer);
    }
}
