using System;
using System.Collections.Generic;

namespace Main.Scripts.EventSystem
{
    public static class Dispatcher
    {
        private static readonly Dictionary<Type, List<Delegate>> _eventTable = new Dictionary<Type, List<Delegate>>();

        /// <summary>
        /// Subscribe to Dispatch List
        /// </summary>
        /// <param name="listener">Listener Method</param>
        public static void Subscribe<T>(Action<T> listener)
        {
            var type = typeof(T);
            if (!_eventTable.ContainsKey(type))
            {
                _eventTable[type] = new List<Delegate>();
            }

            _eventTable[type].Add(listener);
        }

        /// <summary>
        /// Unubscribe to Dispatch List
        /// </summary>
        /// <param name="listener">Listener Method</param>
        public static void Unsubscribe<T>(Action<T> listener)
        {
            var type = typeof(T);
            if (_eventTable.TryGetValue(type, out var listeners))
            {
                listeners.Remove(listener);
                if (listeners.Count == 0)
                {
                    _eventTable.Remove(type);
                }
            }
        }

        /// <summary>
        /// Dispatches Event For All Listeners of Type T
        /// </summary>
        /// <param name="evt">Event Event</param>
        public static void Dispatch<T>(T evt)
        {
            var type = typeof(T);
            if (_eventTable.TryGetValue(type, out var listeners))
            {
                var listenersCopy = listeners.ToArray();
                foreach (var listener in listenersCopy)
                {
                    ((Action<T>)listener)?.Invoke(evt);
                }
            }
        }

        /// <summary>
        /// Clears All Events
        /// Similar to RemoveAllListeners for Unity Events
        /// </summary>
        public static void Clear()
        {
            _eventTable.Clear();
        }
    }
}
