using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Main.Gameplay
{
    public class Shelf : MonoBehaviour
    {
        [SerializeField] private Transform[] points;

        [SerializeField] private Vector3 startScale;
        [SerializeField] private float dur;

        [SerializeField] private Vector3 productScale;
        [SerializeField] private float productDur;
        [SerializeField] private float productAnimGap;

        private List<Product> _products = new();
        private Action OnAnimCompleted;

        void Clear()
        {
            foreach (var p in _products)
            {
                if (p == null)
                    continue;
                Destroy(p.gameObject);
            }

            _products.Clear();
        }

        public void SetOnComplete(Action a)
        {
            OnAnimCompleted = a;
        }

        public void Create(List<Product> products)
        {
            Clear();

            for (int i = 0; i < points.Length; i++)
            {
                var point = points[i];
                var prod = products[i];
                if (prod is not Product p)
                    continue;

                p.transform.SetParent(point);
                p.transform.localPosition = Vector3.zero;
                p.transform.localScale = Vector3.zero;

                _products.Add(p);
            }
            StartCoroutine(ShelfAnimation());
        }

        public void Refresh(List<Product> products)
        {
            Clear();

            for (int i = 0; i < points.Length; i++)
            {
                var point = points[i];
                var p = products[i];

                p.transform.SetParent(point);
                p.transform.localPosition = Vector3.zero;
                p.transform.localScale = Vector3.zero;

                _products.Add(p);
            }
            StartCoroutine(ShelfAnimation());
        }

        IEnumerator RefreshAnimation()
        {
            foreach (var p in _products)
            {
                StartCoroutine(ProductAnimation(p));
                yield return new WaitForSeconds(productAnimGap);
            }
            OnAnimCompleted?.Invoke();
            yield break;
        }

        IEnumerator ShelfAnimation()
        {
            var start = Time.time;
            transform.localScale = startScale;
            while (Time.time - start < dur)
            {
                var diff = Time.time - start;
                var t = diff / dur;

                transform.localScale = Vector3.Lerp(startScale, Vector3.one, t);
                yield return null;

            }
            transform.localScale = Vector3.one;

            foreach (var p in _products)
            {
                StartCoroutine(ProductAnimation(p));
                yield return new WaitForSeconds(productAnimGap);
            }
            OnAnimCompleted?.Invoke();
            yield break;
        }

        IEnumerator ProductAnimation(Product p)
        {
            p.transform.localScale = productScale;
            var start = Time.time;

            while (Time.time - start < productDur)
            {
                var diff = Time.time - start;
                var t = diff / productDur;
                p.transform.localScale = Vector3.Lerp(productScale, Vector3.one, t);
                yield return null;
            }
            p.transform.localScale = Vector3.one;
            yield break;
        }
    }
}
