using Main.Scripts.Events;
using Main.Scripts.EventSystem;
using UnityEngine;
using Zenject;

public class Destroyer : MonoBehaviour
{
    [Inject] private ProductFallManager _fallManager;

    public void OnTriggerEnter2D(Collider2D col)
    {
        var rb = col.attachedRigidbody;
        if (!rb)
            return;

        var p = rb.GetComponent<Product>();
        if (!p)
            return;

        _fallManager.RemoveProduct(p);
        Dispatcher.Dispatch<ChangeHealthEvent>(new ChangeHealthEvent() { Increment = -1 });
        Destroy(p.gameObject);
    }
}
