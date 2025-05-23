using System.Collections;
using Anim.UnityBindings;
using Main.Containers;
using UnityEngine;
using UnityEngine.UI;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;
using Zenject;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image fillTarget;
    [SerializeField] private int maxVal = 50;
    [SerializeField] private float dur = 0.2f;
    [SerializeField] private CanvasGroup group;
    [Inject] private PlayerEconomy _economy;
    private Coroutine _cor;

    void OnEnable()
    {
        _economy.Subscribe(HandleChange);
        Dispatcher.Subscribe<StartPressedEvent>(HandleStart);
        Dispatcher.Subscribe<GameRestartEvent>(HandleRestart);
        Dispatcher.Subscribe<GameFailEvent>(HandleFail);
        Dispatcher.Subscribe<GameSuccessEvent>(HandleSuccess);
    }


    void OnDisable()
    {
        _economy.Unsubscribe(HandleChange);
        Dispatcher.Unsubscribe<StartPressedEvent>(HandleStart);
        Dispatcher.Unsubscribe<GameRestartEvent>(HandleRestart);
        Dispatcher.Unsubscribe<GameFailEvent>(HandleFail);
        Dispatcher.Unsubscribe<GameSuccessEvent>(HandleSuccess);
    }

    private void HandleSuccess(GameSuccessEvent @event)
    {
        group.FadeAnim(0f,.3f).Start();
    }

    private void HandleFail(GameFailEvent @event)
    {
        group.FadeAnim(0f,.3f).Start();
    }

    private void HandleRestart(GameRestartEvent @event)
    {
        HandleChange(0);
    }

    private void HandleStart(StartPressedEvent @event)
    {
        group.FadeAnim(1f,.3f).Start();
    }

    void HandleChange(int val)
    {
        if (_cor != null)
            StopCoroutine(_cor);

        var p = (float)val / maxVal;

        _cor = StartCoroutine(AnimCor(p));
    }

    IEnumerator AnimCor(float p)
    {
        var t = Time.time; ;
        while (Time.time - t < dur)
        {
            var diff = Time.time - t;
            var perc = diff / dur;

            fillTarget.fillAmount = Mathf.Lerp(fillTarget.fillAmount, p, perc);
            yield return null;
        }
        fillTarget.fillAmount = p;
    }
}
