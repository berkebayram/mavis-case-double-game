
using UnityEngine;
using Anim.UnityBindings;
using Anim.Ease;

public class BaloonCosmetic : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotation;

    void Start()
    {
        RotateRecuversively();
    }

    void RotateRecuversively()
    {
        var anim = target.RotateAnim( Quaternion.AngleAxis(rotation, Vector3.forward) * transform.rotation, 1f);
        anim.SetOnComplete(() =>
        {
            RotateRecuversively();
        });
        anim.Start();
    }
}
