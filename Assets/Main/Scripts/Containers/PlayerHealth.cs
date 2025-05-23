using System;
using System.Collections.Generic;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;

namespace Main.Containers
{
    public class PlayerHealth
    {

        private int _current = 3;
        public int Current => _current;
        List<Action<int>> _delegates = new();

        private PlayerHealth()
        {
            Dispatcher.Subscribe<ChangeHealthEvent>(HandleChange);
            Dispatcher.Subscribe<GameRestartEvent>(HandleRestart);
        }

        public void Dispose()
        {
            Dispatcher.Unsubscribe<ChangeHealthEvent>(HandleChange);
            Dispatcher.Unsubscribe<GameRestartEvent>(HandleRestart);
        }

        private void HandleRestart(GameRestartEvent @event)
        {
            HandleChange(new ChangeHealthEvent(){Increment =  3});
        }

        void HandleChange(ChangeHealthEvent @event)
        {
            _current += @event.Increment;

            foreach (var d in _delegates)
                d?.Invoke(_current);

            if (_current == 0)
                Dispatcher.Dispatch<GameFailEvent>(new GameFailEvent());
        }

        public void Subscribe(Action<int> listener)
        {
            _delegates.Add(listener);
        }

        public void Unsubscribe(Action<int> listener)
        {
            _delegates.Remove(listener);
        }
    }
}
