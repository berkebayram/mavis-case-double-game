using UnityEngine;
using Anim.UnityBindings;
using Anim.Ease;

public class SunAnimator : MonoBehaviour
{
    [SerializeField] private float scale;
    [SerializeField] private float scaleDur;

    [SerializeField] private float rotation;

    void Start()
    {
        transform.localScale = Vector3.one * .35f;
        transform.ScaleAnim(Vector3.one * scale, scaleDur, 0f, new OutQuadEaser()).Start();

        RotateRecuversively();
    }

    void RotateRecuversively()
    {
        var anim = transform.RotateAnim( Quaternion.AngleAxis(rotation, Vector3.forward) * transform.rotation, 1f);
        anim.SetOnComplete(() =>
        {
            RotateRecuversively();
        });
        anim.Start();
    }
}
