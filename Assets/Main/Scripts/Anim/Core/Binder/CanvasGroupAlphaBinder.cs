
using UnityEngine;
using UnityEngine.UI;

namespace Anim.Binder
{
    public class CanvasGroupAlphaBinder : IAnimBinder<float>
    {
        private CanvasGroup _gui;
        public CanvasGroupAlphaBinder(CanvasGroup gui)
        {
            _gui = gui;
        }

        public float Get()
        {
            return _gui.alpha;
        }

        public void Set(float val)
        {
            _gui.alpha = val;
        }
    }
}
