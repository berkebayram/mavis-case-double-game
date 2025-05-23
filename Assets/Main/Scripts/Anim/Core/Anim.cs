using System;
using System.Collections;
using Anim.Binder;
using Anim.UnityBindings;
using Anim.Calculator;
using Anim.Ease;

namespace Anim
{
    public class Anim<V> : IAnim<V>, IAnimationEventHandler, IPlayable
    {
        public IAnimEaser Easer { get; private set; }
        public IAnimBinder<V> Binder { get; private set; }
        public IAnimCalculator<V> Calculator { get; private set; }

        public float Duration { get; private set; }
        public float Delay { get; private set; }
        public bool IsAnimating { get; private set; }
        public bool IsFinished { get; private set; }
        public int Id { get; private set; }

        public Action OnComplete { get; private set; }
        public Action OnStart { get; private set; }

        private float _curr;
        private bool _isRegistered;

        static Anim()
        {
            var runner = TweenRunner.Instance;
        }

        public Anim(
                IAnimEaser easer,
                IAnimBinder<V> binder,
                IAnimCalculator<V> calculator,
                float duration,
                float delay
                )
        {
            Easer = easer;
            Binder = binder;
            Calculator = calculator;
            Duration = duration;
            Delay = delay;
            Id = TweenRunner.GetId();
        }
        public void Start()
        {
            if (!_isRegistered)
            {
                OnStart?.Invoke();
                TweenRunner.Instance.Register(this);
                _isRegistered = true;
            }
            IsAnimating = true;
        }

        public void Step(float delta)
        {
            try{
            _curr += delta;
            if (_curr < Delay)
                return;

            var p = Math.Min(1f, (_curr - Delay) / Duration); // progression
            p = Easer.Ease(p); // Eased
            var val = Calculator.Calculate(p);
            Binder.Set(val);

            if (p == 1f)// Finished
            {
                Destroy();
                OnComplete?.Invoke();
            }
            }
            catch
            {
                Destroy();
            }
        }

        public void Stop()
        {
            IsAnimating = false;
        }

        public void Destroy()
        {
            IsFinished = true;
            IsAnimating = false;
            TweenRunner.Instance.Destroy(Id);
        }

        public IEnumerator GetCoroutine(Func<float> deltaTimer)
        {
            IsAnimating = true;
            OnStart?.Invoke();
            float dt = 0;
            while (Delay > 0)
            {
                dt = deltaTimer();
                Delay -= dt;
                yield return null;
            }

            dt = Delay < 0 ? -1f * Delay : dt;

            while (IsAnimating)
            {
                dt = dt <= 0 ? deltaTimer() : dt;
                Step(dt);
                dt = 0f;
                yield return null;
            }
            yield break;
        }

        public void SetOnComplete(Action action)
        {
            OnComplete = action;
        }

        public void SetOnStart(Action action)
        {
            OnStart = action;
        }
    }
}
