using UnityEngine;
using TMPro;
using Anim.UnityBindings;
using Anim.Ease;
using Main.Containers;
using Zenject;

public class MoneyDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gui;
    [SerializeField] private Transform animTarget;

    [Inject] private PlayerEconomy _economy;
    private int _animId = -1;

    void OnEnable()
    {
        _economy.Subscribe(HandleEconomy);
    }

    void OnDisable()
    {
        _economy.Unsubscribe(HandleEconomy);
    }

    void Start()
    {
        gui.SetText("0");
    }

    public void HandleEconomy(int val)
    {
        gui.SetText(val.ToString());
        if (_animId != -1)
            TweenRunner.Instance.Destroy(_animId);

        animTarget.localScale = Vector3.one;
        var anim = animTarget.ScaleAnim(1.2f * Vector3.one, .15f,0f,new InQuadEaser());
        anim.SetOnComplete(() =>
        {
            var secondAnim = animTarget.ScaleAnim(1f * Vector3.one, .15f, 0f, new InQuadEaser());
            _animId = anim.Id;
            secondAnim.Start();
        });
        _animId = anim.Id;
        anim.Start();
    }

}
