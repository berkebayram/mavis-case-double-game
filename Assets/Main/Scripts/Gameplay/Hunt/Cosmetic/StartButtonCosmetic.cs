using UnityEngine;
using UnityEngine.UI;
using Anim.UnityBindings;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;

public class StartButtonCosmetic : MonoBehaviour
{
    [SerializeField] private Vector2 minMaxScale;
    [SerializeField] private float animDur = 0.666f;
    [SerializeField] private Button btn;

    void OnEnable()
    {
        Dispatcher.Subscribe<GameRestartEvent>(HandleRestart);
    }

    void OnDisable()
    {
        Dispatcher.Unsubscribe<GameRestartEvent>(HandleRestart);
    }

    void HandleRestart(GameRestartEvent @event)
    {
        btn.gameObject.SetActive(true);
    }

    void Start()
    {
        btn.transform.localScale = Vector3.one * minMaxScale.x;
        ScaleRecuversively(true);
        btn.onClick.AddListener(HandleClick);
    }

    void HandleClick()
    {
        Dispatcher.Dispatch<StartPressedEvent>(new StartPressedEvent());
        btn.gameObject.SetActive(false);
    }

    void ScaleRecuversively(bool isGrowing)
    {
        var anim = btn.transform.ScaleAnim(isGrowing ? Vector3.one * minMaxScale.y : Vector3.one * minMaxScale.x, animDur);
        anim.SetOnComplete(() =>
        {
            ScaleRecuversively(!isGrowing);
        });
        anim.Start();
    }

}
