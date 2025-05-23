using UnityEngine;

namespace Anim.Binder
{
    public class RotationBinder : IAnimBinder<Quaternion>
    {
        private Transform _transform;
        public RotationBinder(Transform t)
        {
            _transform = t;
        }

        public Quaternion Get()
        {
            return _transform.rotation;
        }

        public void Set(Quaternion val)
        {
            _transform.rotation = val;
        }
    }
}
