using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Main.Containers;
using Main.Scripts.Events;
using Zenject;

public class HealthDisplayer : MonoBehaviour
{
    [SerializeField] private Sprite empty;
    [SerializeField] private Sprite full;
    [SerializeField] private List<Image> images;
    [Inject] private PlayerHealth _health;

    void OnEnable()
    {
        _health.Subscribe(SetHeart);
    }

    void OnDisable()
    {
        _health.Unsubscribe(SetHeart);
    }

    void HandleRestart(GameRestartEvent e)
    {
        SetHeart(3);
    }

    void Start()
    {
        SetHeart(3);
    }

    private void SetHeart(int heartCount)
    {
        for (int i = 0; i < images.Count; i++)
        {
            var img = images[i];
            img.sprite = i < heartCount ? full : empty;
        }
    }
}
