using System;
using System.Collections;
using System.Collections.Generic;
using Main.Scripts.EventSystem;
using Main.Scripts.Events;
using Main.Gameplay;
using UnityEngine;
using Zenject;

public class ShelfManager : MonoBehaviour
{
    [SerializeField] private Shelf prefab;
    [Inject] private ProductManager productFactory;
    [Inject] private ProductFallManager fallManager;
    [Inject] private ShelfManagerSettings settings;

    private List<Shelf> _shelves = new();

    void OnEnable()
    {
        Dispatcher.Subscribe<GameRestartEvent>(HandleRestart);
        Dispatcher.Subscribe<ChangeLevelEvent>(HandleNewLevel);
    }


    void OnDisable()
    {
        Dispatcher.Subscribe<GameRestartEvent>(HandleRestart);
        Dispatcher.Unsubscribe<ChangeLevelEvent>(HandleNewLevel);
    }

    private void HandleNewLevel(ChangeLevelEvent @event)
    {
        StartCoroutine(GenerateProducts(settings.ProductCount));
    }

    void HandleRestart(GameRestartEvent @event)
    {
        StartCoroutine(GenerateProducts(settings.ProductCount));
    }

    void Start()
    {
        StartCoroutine(GenerateShelves(settings.ShelfCount, settings.ProductCount));
    }

    IEnumerator GenerateProducts(int prodCount)
    {
        for (int i = 0; i < _shelves.Count; i++)
        {
            var shelf = _shelves[i];
            var products = productFactory.GetProducts(prodCount);
            shelf.Create(products);

            foreach (var p in products)
                fallManager.Add(p);

            if (i == _shelves.Count - 1)
                shelf.SetOnComplete(() =>
                {
                    Dispatcher.Dispatch<GameStartEvent>(new GameStartEvent());
                });
            yield return new WaitForSeconds(.1f);
        }
        yield break;
    }

    IEnumerator GenerateShelves(int shelfCount, int prodCount)
    {
        for (int i = 0; i < shelfCount; i++)
        {
            var pos = settings.FirstShelfPos + i * settings.PrefabGap;

            var shelf = Instantiate(prefab, pos, Quaternion.identity);
            _shelves.Add(shelf);
            var products = productFactory.GetProducts(prodCount);
            shelf.Create(products);

            foreach (var p in products)
                fallManager.Add(p);

            if (i == shelfCount - 1)
                shelf.SetOnComplete(() =>
                {
                    Dispatcher.Dispatch<GameStartEvent>(new GameStartEvent());
                });
            yield return new WaitForSeconds(.1f);
        }
        yield break;
    }

    void OnDrawGizmosSelected()
    {
        if (settings.ShelfCount <= 0)
            return;

        Gizmos.color = Color.white;
        for (int i = 0; i < settings.ShelfCount; i++)
        {
            var pos = settings.FirstShelfPos + i * settings.PrefabGap;
            Gizmos.DrawCube(pos, Vector3.one);
        }
    }
}
