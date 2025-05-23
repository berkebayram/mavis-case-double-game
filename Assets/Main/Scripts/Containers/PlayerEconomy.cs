using System;
using System.Collections.Generic;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;

namespace Main.Containers
{
    public class PlayerEconomy
    {
        public string Key => "Economy";
        private int _current;
        public int Current => _current;
        private List<Action<int>> _listeners;

        public PlayerEconomy()
        {
            _listeners = new();
            Dispatcher.Subscribe<ChangeMoneyEvent>(HandleChange);
            Dispatcher.Subscribe<GameRestartEvent>(HandleRestart);
        }

        private void HandleRestart(GameRestartEvent @event)
        {
            HandleChange(new ChangeMoneyEvent() { Increment = -1 * Current });
        }

        public void Dispose()
        {
            Dispatcher.Unsubscribe<ChangeMoneyEvent>(HandleChange);
            Dispatcher.Unsubscribe<GameRestartEvent>(HandleRestart);
        }

        void HandleChange(ChangeMoneyEvent @event)
        {
            _current += @event.Increment;

            foreach (var l in _listeners)
                l?.Invoke(_current);
        }

        public void Subscribe(Action<int> onChange)
        {
            _listeners.Add(onChange);
        }

        public void Unsubscribe(Action<int> onChange)
        {
            _listeners.Remove(onChange);
        }

    }
}
