using UnityEngine;

namespace Anim.Binder
{
    public class LocalRotationBinder : IAnimBinder<Quaternion>
    {
        private Transform _transform;
        public LocalRotationBinder(Transform t)
        {
            _transform = t;
        }

        public Quaternion Get()
        {
            return _transform.localRotation;
        }

        public void Set(Quaternion val)
        {
            _transform.localRotation = val;
        }
    }
}
