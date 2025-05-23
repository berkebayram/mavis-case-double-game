using TMPro;
using UnityEngine;
using System;
using Anim.UnityBindings;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;

public class LevelDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gui;
    [SerializeField] private float dur;

    private Action _onComplete;

    void OnEnable()
    {
        Dispatcher.Subscribe<ChangeLevelEvent>(HandleNewLevel);
    }

    void OnDisable()
    {
        Dispatcher.Unsubscribe<ChangeLevelEvent>(HandleNewLevel);
    }

    private void HandleNewLevel(ChangeLevelEvent @event)
    {
        Show(@event.level);
    }

    void Start()
    {
        Show(0);
    }

    public void Show(int level, Action action = null)
    {
        _onComplete = action;
        var fadeIn = gui.FadeAnim(1f, dur);
        fadeIn.SetOnComplete(() =>
        {
            var fadeOut = gui.FadeAnim(0f, dur);
            fadeOut.SetOnComplete(_onComplete);
            fadeOut.Start();
        });
        fadeIn.SetOnStart(() =>
        {
            gui.SetText("Aşama " + level.ToString());
        });
        fadeIn.Start();
    }
}
