using UnityEngine;

namespace Anim.Binder
{
    public class ScaleBinder : IAnimBinder<Vector3>
    {
        private Transform _transform;
        public ScaleBinder(Transform t)
        {
            _transform = t;
        }

        public Vector3 Get()
        {
            return _transform.localScale;
        }

        public void Set(Vector3 val)
        {
            _transform.localScale = val;
        }
    }
}
