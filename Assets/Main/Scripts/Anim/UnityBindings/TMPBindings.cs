
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

using Anim.Ease;
using Anim.Calculator;
using Anim.Binder;

namespace Anim.UnityBindings
{
    public static class TMPBindings
    {
        public static Anim<Color> ColorAnim(
                this TextMeshProUGUI gui,
                Color target,
                float duration,
                float delay = 0f,
                IAnimEaser easer = null,
                IAnimCalculator<Color> calculator = null,
                Action onComplete = null,
                Action onStart = null
                )
        {
            var binder = new ColorBinder(gui);
            easer ??= new LinearEaser();
            calculator ??= new ColorLerper(binder.Get(), target);
            return new Anim<Color>(easer, binder, calculator, duration, delay);
        }
        public static Anim<Color> FadeAnim(
                this TextMeshProUGUI gui,
                float target,
                float duration,
                float delay = 0f,
                IAnimEaser easer = null,
                IAnimCalculator<Color> calculator = null,
                Action onComplete = null,
                Action onStart = null
                )
        {
            var binder = new ColorBinder(gui);
            easer ??= new LinearEaser();

            var targetColor = binder.Get();
            targetColor.a = target;
            calculator ??= new ColorLerper(binder.Get(), targetColor);
            return new Anim<Color>(easer, binder, calculator, duration, delay);
        }


        public static Anim<Color> FadeAnim(
                this Image img,
                float target,
                float duration,
                float delay = 0f,
                IAnimEaser easer = null,
                IAnimCalculator<Color> calculator = null,
                Action onComplete = null,
                Action onStart = null
                )
        {
            var binder = new ImageColorBinder(img);
            easer ??= new LinearEaser();

            var targetColor = binder.Get();
            targetColor.a = target;
            calculator ??= new ColorLerper(binder.Get(), targetColor);
            return new Anim<Color>(easer, binder, calculator, duration, delay);
        }


        public static Anim<float> FadeAnim(
                this CanvasGroup img,
                float target,
                float duration,
                float delay = 0f,
                IAnimEaser easer = null,
                IAnimCalculator<float> calculator = null,
                Action onComplete = null,
                Action onStart = null
                )
        {
            var binder = new CanvasGroupAlphaBinder(img);
            easer ??= new LinearEaser();

            calculator ??= new FloatLerper(binder.Get(), target);
            return new Anim<float>(easer, binder, calculator, duration, delay);
        }
    }
}
