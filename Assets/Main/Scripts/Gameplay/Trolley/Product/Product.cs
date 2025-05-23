using UnityEngine;
using Anim.UnityBindings;
using Main.Gameplay;
using Main.Containers;
using Zenject;

public class Product : MonoBehaviour, IProduct
{
    [SerializeField] private PopMoney popMoney;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private string collidableLayer;
    [SerializeField] private string unCollidableLayer;
    [SerializeField] private GameObject[] layerTargets;

    public bool IsDestroying { get; private set; }

    public void Fall()
    {
        rb.isKinematic = false;
        foreach (var target in layerTargets)
            target.layer = LayerMask.NameToLayer(collidableLayer);
    }

    public void TransformApart(ProductFallManager fallManager, GameLevel _level)
    {
        var gameLevel = _level.Current;

        IsDestroying = true;
        foreach (var target in layerTargets)
            target.layer = LayerMask.NameToLayer(unCollidableLayer);

        var moneyTarget = GameObject.FindObjectOfType<MoneyTarget>(true);

        var anim = transform.ScaleAnim(Vector3.one * .5f, .2f);
        anim.SetOnComplete(() =>
        {
            for (int i = 0; i < gameLevel.productPrize; i++)
            {
                var go = Instantiate(popMoney, transform.position, transform.rotation);
                var dispDir = new Vector2(
                        Mathf.Cos(Mathf.Deg2Rad * i * (120f + Random.value * 50)),
                        Mathf.Sin(Mathf.Deg2Rad * i * (120f + Random.value * 50)));

                go.Setup(
                        moneyTarget.transform.position,
                        .4f,
                        .1f,
                        dispDir.normalized * 0.75f,
                        1);
            }

            fallManager.RemoveProduct(this);
            Destroy(gameObject);
        });
        anim.Start();
    }
}
