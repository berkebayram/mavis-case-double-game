using UnityEngine;
using System.Collections.Generic;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;

public class ProductManager : MonoBehaviour
{
    [SerializeField] private List<Product> products;

    public List<Product> GetProducts(int count)
    {
        var res = new List<Product>();
        for (int i = 0; i < count; i++)
        {
            var randomPrefab = products[Random.Range(0, products.Count)];
            res.Add(Instantiate(randomPrefab));
        }
        return res;
    }
}
