using System.Collections.Generic;
using Anim.Calculator;
using UnityEngine;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;

public class BaloonFactory : MonoBehaviour
{
    [SerializeField] private List<BaloonData> data;
    [SerializeField] private List<BaloonRoute> routeData;
    [SerializeField] private Baloon prefab;

    void OnEnable()
    {
        Dispatcher.Subscribe<SpawnBaloonEvent>(HandleSpawn);
    }

    private void HandleSpawn(SpawnBaloonEvent @event)
    {
        var go = Instantiate(prefab);
        var d = data[(int)@event.BaloonLevel];
        go.Setup(d);
        go.SetRoute(GetRouteRandom());
    }

    void OnDisable()
    {
        Dispatcher.Unsubscribe<SpawnBaloonEvent>(HandleSpawn);
    }



    public Baloon GetBaloon(BaloonType baloon)
    {
        var d = data[(int)baloon];
        var obj = Instantiate(prefab);
        obj.Setup(d);
        return obj;
    }

    public BaloonRoute GetRouteRandom()
    {
        return routeData[Random.Range(0, routeData.Count)];
    }

    void OnDrawGizmosSelected()
    {
        if (routeData == null)
            return;
        foreach (var route in routeData)
        {
            foreach (var d in route.pieces)
            {
                if (d.RouteType == BaloonRouteType.Linear)
                {
                    Gizmos.DrawLine(d.Start, d.End);
                }
                else
                {
                    var calc = new QuadraticBezierCalculator2D((Vector2)d.Start, (Vector2)d.MiddlePoint, (Vector2)d.End);
                    var before = calc.Calculate(0f);

                    for (int i = 1; i < 100; i++)
                    {
                        var current = calc.Calculate(i / 100f);
                        Gizmos.DrawLine(before, current);
                        before = current;
                    }

                }
            }

        }
    }
}
