using System.Collections.Generic;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;

namespace Main.Containers
{
    public class GameLevel
    {
        private List<ChangeLevelEvent> _levels;
        private int _c;
        public ChangeLevelEvent Current => _levels[_c];

        private GameLevel()
        {
            _levels = new();
            _c = 0;
        }

        public void Setup(List<ChangeLevelEvent> levels)
        {
            _levels = levels;
        }

        void Reset()
        {
            _c = 0;
        }

        public bool HasLevel()
        {
            return _c + 1 < _levels.Count;
        }

        public void LevelUp()
        {
            _c++;
            Dispatcher.Dispatch<ChangeLevelEvent>(_levels[_c]);
        }
    }
}
