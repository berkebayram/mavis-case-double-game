using UnityEngine;
using Anim.UnityBindings;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;
using Zenject;

public class FtueController : MonoBehaviour
{
    [Inject] private FtueHand hand;
    [Inject] private BaloonFactory factory;
    [Inject] private Aim aim;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 baloonPos;

    void OnEnable()
    {
        Dispatcher.Subscribe<StartPressedEvent>(HandleStartClicked);
    }

    void OnDisable()
    {
        Dispatcher.Unsubscribe<StartPressedEvent>(HandleStartClicked);
    }

    void HandleStartClicked(StartPressedEvent @event)
    {
        var baloon = factory.GetBaloon(BaloonType.Large);
        baloon.transform.position = baloonPos;

        hand.gameObject.SetActive(true);
        hand.transform.position = aim.transform.position + offset;
        hand.Follow(aim.transform);


        var mag = (aim.transform.position - baloonPos).magnitude;
        var anim = aim.transform.MoveAnimBezier(baloonPos, .2f * mag * GetPerpendicularUnitVector(transform.position, baloonPos), 1f);
        anim.SetOnStart(() =>
        {
            aim.SetInput(false);
            aim.gameObject.SetActive(true);
        });
        anim.SetOnComplete(() =>
        {
            aim.SetInput(true);
            hand.gameObject.SetActive(false);
            aim.Shoot();
        });
        anim.Start();
    }

    public Vector3 GetPerpendicularUnitVector(Vector3 from, Vector3 to)
    {
        var direction = to - from;
        var perpendicular = new Vector3(-direction.y, direction.x, direction.z);
        return perpendicular.normalized;
    }
}
