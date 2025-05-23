using System.Collections.Generic;
using Main.Containers;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;
using UnityEngine;
using Zenject;

public class ProductFallManager : MonoBehaviour
{
    [Inject] private GameLevel _level;
    private List<Product> _products;
    private List<Product> _onAir;
    private bool _isActive;
    private float _last;

    void OnEnable()
    {
        Dispatcher.Subscribe<GameFailEvent>(HandleFail);
        Dispatcher.Subscribe<ChangeLevelEvent>(StopFalling);
        Dispatcher.Subscribe<GameStartEvent>(StartFalling);
    }

    void OnDisable()
    {
        Dispatcher.Unsubscribe<GameFailEvent>(HandleFail);
        Dispatcher.Unsubscribe<ChangeLevelEvent>(StopFalling);
        Dispatcher.Unsubscribe<GameStartEvent>(StartFalling);
    }

    void StartFalling(GameStartEvent e)
    {
        _isActive = true;
    }

    void StopFalling()
    {
        _isActive = false;
    }

    void HandleFail(GameFailEvent e)
    {
        var objs = GameObject.FindObjectsOfType<Product>();
        for (int i = 0; i < objs.Length; i++)
        {
            var p = objs[i];
            Destroy(p.gameObject);
        }

        // I Tried animation but it sucks Ahahah
        // for (int i = 0; i < _products.Count; i++)
        // {
        //     var p = _products[i];
        //     var anim = p.transform.ScaleAnim(Vector3.one * 0.75f, 0.25f, i * 50f);
        //     var o = p.gameObject;
        //     Destroy(p);
        //     anim.SetOnComplete(() =>
        //     {
        //         Destroy(o);
        //     });
        //     anim.Start();
        // }
        //
        // for (int i = 0; i < _onAir.Count; i++)
        // {
        //     var p = _onAir[i];
        //     var anim = p.transform.ScaleAnim(Vector3.one * 0.75f, 0.25f, i * 50f);
        //     var o = p.gameObject;
        //     Destroy(p);
        //     anim.SetOnComplete(() =>
        //     {
        //         Destroy(o);
        //     });
        //     anim.Start();
        // }
        //
        _products.Clear();
        _onAir.Clear();

        StopFalling();
    }


    void StopFalling(ChangeLevelEvent e)
    {
        StopFalling();
    }

    void Update()
    {
        if (!_isActive)
            return;

        var lv = _level.Current;
        if (Time.time - _last >= lv.cooldown)
        {
            _last = Time.time;
            DropRandom();
        }
    }

    public void CheckFinished()
    {
        if (_onAir.Count == 0 && _products.Count == 0)
            Dispatcher.Dispatch<NoProductEvent>(new NoProductEvent());
    }

    void DropRandom()
    {
        _products ??= new();
        _onAir ??= new();

        if (_products.Count == 0)
        {
            return;
        }

        var index = Random.Range(0, _products.Count);
        var p = _products[index];
        if (p == null)
            return;

        p.Fall();
        _products.RemoveAt(index);
    }

    public void Clear()
    {
        foreach (var product in _onAir)
        {
            if (product != null)
                Destroy(product.gameObject);

        }
        _onAir.Clear();

        foreach (var product in _products)
        {
            if (product != null)
                Destroy(product.gameObject);

        }
        _products.Clear();
    }

    public void RemoveProduct(Product p)
    {
        if (_onAir.Contains(p))
            _onAir.Remove(p);
        CheckFinished();
    }

    public void Add(Product p)
    {
        _products ??= new();
        _onAir ??= new();
        _products.Add(p);
        _onAir.Add(p);
    }
}
