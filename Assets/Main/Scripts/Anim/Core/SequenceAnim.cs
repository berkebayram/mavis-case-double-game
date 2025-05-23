using System;
using Anim.UnityBindings;

namespace Anim
{
    public class SequenceAnim : IPlayable, IAnimationEventHandler
    {
        public float Duration { get; private set; }
        public float Delay { get; private set; }
        public bool IsAnimating { get; private set; }
        public bool IsFinished { get; private set; }
        public int Id { get; private set; }

        public Action OnComplete { get; private set; }
        public Action OnStart { get; private set; }

        private IPlayable[] _playables;
        private bool _isRegistered;
        private float _curr;
        private float _gap;
        private IPlayable _currentPlayable;
        private int _index;

        public SequenceAnim(
            float delay = 0f,
            float gap = 0f,
            Action onStart = null,
            Action onComplete = null,
            params IPlayable[] playables
            )
        {
            _playables = playables;
            _gap = gap;
            Delay = delay;
            OnStart = onStart;
            OnComplete = onComplete;

            foreach (var p in playables)
            {
                Duration += p.Duration;
            }

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
            _curr += delta;
            if (_curr < Delay)
                return;

            if (_currentPlayable == null)
            {
                _currentPlayable = _playables[_index];
                _currentPlayable.Start();
                return;
            }

            if (!_currentPlayable.IsAnimating && !_currentPlayable.IsFinished)
            {
                _currentPlayable.Start();
                return;
            }

            if (_currentPlayable.IsFinished)
            {
                if (_index + 1 < _playables.Length)
                {
                    _index++;
                    _currentPlayable = _playables[_index];
                    _curr = 0f;
                    Duration = _gap;
                    Step(delta);
                }
                else
                {
                    IsFinished = true;
                    OnComplete?.Invoke();
                    Destroy();
                }
            }

        }

        public void Stop()
        {
            IsAnimating = false;
        }

        public void Destroy()
        {
            TweenRunner.Instance.Destroy(Id);
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
