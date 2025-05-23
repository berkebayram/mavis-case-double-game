using UnityEngine;

namespace Anim.Binder
{
    public class PositionBinder : IAnimBinder<Vector3>
    {
        private Transform _transform;
        public PositionBinder(Transform t)
        {
            _transform = t;
        }

        public Vector3 Get()
        {
            return _transform.position;
        }

        public void Set(Vector3 val)
        {
            _transform.position = val;
        }
    }
}
