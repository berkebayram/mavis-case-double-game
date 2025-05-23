using UnityEngine;
using TMPro;

namespace Anim.Binder
{
    public class ColorBinder : IAnimBinder<Color>
    {
        private TextMeshProUGUI _gui;
        public ColorBinder(TextMeshProUGUI gui)
        {
            _gui = gui;
        }

        public Color Get()
        {
            return _gui.color;
        }

        public void Set(Color val)
        {
            _gui.color = val;
        }
    }
}
