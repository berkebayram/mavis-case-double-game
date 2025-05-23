using System;
using UnityEngine;
using Anim;
using Anim.Ease;
using Anim.Calculator;
using Anim.Binder;

namespace Anim.UnityBindings
{
    public static class TransformBindings
    {

        public static Anim<Quaternion> RotateAnim(
                this Transform transform,
                Quaternion target,
                float duration,
                float delay = 0f,
                IAnimEaser easer = null,
                IAnimCalculator<Quaternion> calculator = null,
                Action onComplete = null,
                Action onStart = null
                )
        {
            var binder = new RotationBinder(transform);
            easer ??= new LinearEaser();
            calculator ??= new QuaternionLerper(binder.Get(), target);
            return new Anim<Quaternion>(easer, binder, calculator, duration, delay);
        }

        public static Anim<Vector3> MoveAnim(
                this Transform transform,
                Vector3 target,
                float duration,
                float delay = 0f,
                IAnimEaser easer = null,
                IAnimCalculator<Vector3> calculator = null,
                Action onComplete = null,
                Action onStart = null
                )
        {
            var binder = new PositionBinder(transform);
            easer ??= new LinearEaser();
            calculator ??= new Vector3Lerper(binder.Get(), target);
            return new Anim<Vector3>(easer, binder, calculator, duration, delay);
        }
        public static Anim<Vector3> MoveAnimBezier(
                this Transform transform,
                Vector3 target,
                Vector3 middle,
                float duration,
                float delay = 0f,
                IAnimEaser easer = null,
                Action onComplete = null,
                Action onStart = null
                )
        {
            var binder = new PositionBinder(transform);
            easer ??= new LinearEaser();
            var calculator = new QuadraticBezierCalculator2D(binder.Get(), middle, target);
            return new Anim<Vector3>(easer, binder, calculator, duration, delay);
        }

        public static Anim<Vector3> LocalMoveAnim(
                this Transform transform,
                Vector3 target,
                float duration,
                float delay = 0f,
                IAnimEaser easer = null,
                IAnimCalculator<Vector3> calculator = null,
                Action onComplete = null,
                Action onStart = null
                )
        {
            var binder = new LocalPositionBinder(transform);
            easer ??= new LinearEaser();
            calculator ??= new Vector3Lerper(binder.Get(), target);
            return new Anim<Vector3>(
                    easer,
                    binder,
                    calculator,
                    duration,
                    delay
                    );
        }

        public static Anim<Vector3> ScaleAnim(
                this Transform transform,
                Vector3 target,
                float duration,
                float delay = 0f,
                IAnimEaser easer = null,
                IAnimCalculator<Vector3> calculator = null,
                Action onComplete = null,
                Action onStart = null
                )
        {
            var binder = new ScaleBinder(transform);
            easer ??= new LinearEaser();
            calculator ??= new Vector3Lerper(binder.Get(), target);
            return new Anim<Vector3>(
                    easer,
                    binder,
                    calculator,
                    duration,
                    delay
                    );
        }
    }
}
