using UnityEngine;
using UnityEngine.UI;

namespace Anim.Binder
{
    public class ImageColorBinder : IAnimBinder<Color>
    {
        private Image _gui;
        public ImageColorBinder(Image gui)
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
