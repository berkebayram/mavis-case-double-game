using UnityEngine;
using Anim.UnityBindings;

using Main.Scripts.Events;
using Main.Scripts.EventSystem;

public class PopMoney : MonoBehaviour
{
    public void Setup(Vector2 target, float duration, float dispersion, Vector2 dispersionDir, int money)
    {
        transform.ScaleAnim(
                Vector3.one * .25f,
                0.2f,
                0f
                ).Start();
        var firstMovement = transform.MoveAnim((Vector2)transform.position + dispersionDir, dispersion, Random.value * 0.3f);
        firstMovement.SetOnComplete(() =>
        {
            var mag = ((Vector2)transform.position - target).magnitude;
            var secondAnim = transform.MoveAnimBezier(target, .2f * mag * GetPerpendicularUnitVector(transform.position, target), duration);
            secondAnim.SetOnComplete(() =>
            {
                Dispatcher.Dispatch(new ChangeMoneyEvent() { Increment = money });
                Destroy(gameObject);
            });
            secondAnim.Start();
        });
        firstMovement.Start();
    }

    public Vector3 GetPerpendicularUnitVector(Vector3 from, Vector3 to)
    {
        var direction = to - from;
        var perpendicular = new Vector3(-direction.y, direction.x, direction.z);
        return perpendicular.normalized;
    }
}
