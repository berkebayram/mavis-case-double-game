
using UnityEngine;

namespace Anim.Binder
{
    public class LocalPositionBinder : IAnimBinder<Vector3>
    {
        private Transform _transform;
        public LocalPositionBinder(Transform t)
        {
            _transform = t;
        }

        public Vector3 Get()
        {
            return _transform.localPosition;
        }

        public void Set(Vector3 val)
        {
            _transform.localPosition = val;
        }
    }
}
