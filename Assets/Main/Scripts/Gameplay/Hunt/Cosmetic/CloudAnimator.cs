using UnityEngine;
using Anim.UnityBindings;
using Anim.Ease;

public class CloudAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 dir;
    [SerializeField] private float dur;
    [SerializeField] private float scaleDur;

    void Start()
    {
        transform.ScaleAnim(Vector3.one, scaleDur,0f,new OutQuadEaser()).Start();
        MoveRecursively(true);
    }

    void MoveRecursively(bool isRight)
    {
        var target = isRight? transform.position + dir : transform.position - dir;
        var anim = transform.MoveAnim( target, dur, 0f);
        anim.SetOnComplete( () => {
                MoveRecursively(!isRight);
                });
        anim.Start();
    }
}
