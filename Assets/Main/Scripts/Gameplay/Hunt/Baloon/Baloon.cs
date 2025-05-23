using UnityEngine;
using Anim.UnityBindings;
using TMPro;
using Anim;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;

public enum BaloonType
{
    Small = 0,
    Medium = 1,
    Large = 2,
}

public class Baloon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gui;
    [SerializeField] private Transform rotateTarget;
    private BaloonData _data;
    private BaloonRoute _route;
    public int Value => _data.Value;
    private int _routeIndex;
    private Anim<Quaternion> _rotateAnim;
    private Anim<Vector3> _moveAnim;
    private Anim<Vector3> _destAnim;

    public void StopAnimation()
    {
        _rotateAnim?.Stop();
        _rotateAnim?.Destroy();

        _moveAnim?.Stop();
        _moveAnim?.Destroy();

        _destAnim?.Stop();
        _destAnim?.Destroy();
    }

    public void Boom()
    {
        StopAnimation();
        var destroyAnim = transform.ScaleAnim(transform.localScale * .75f, .1f);
        destroyAnim.SetOnComplete(() =>
        {
            TweenRunner.Instance.Destroy(destroyAnim.Id);
            Destroy(gameObject);
            Dispatcher.Dispatch(new BaloonPopEvent());
        });
        destroyAnim.Start();
    }


    public void Setup(BaloonData data)
    {
        _data = data;
        gui.SetText($"+{_data.Value}");
        _destAnim = transform.ScaleAnim(transform.localScale * .75f, .3f, _data.duration);
        _destAnim.SetOnComplete(() =>
        {
            Dispatcher.Dispatch(new BaloonPopEvent());
            StopAnimation();
            Destroy(gameObject);
        });
        _destAnim.Start();

        RotateRecursively();
    }

    public void SetRoute(BaloonRoute route)
    {
        _route = route;
        HandleRouteRecursively();
    }

    void RotateRecursively()
    {
        _rotateAnim = rotateTarget.RotateAnim(
                Quaternion.AngleAxis(120f, Vector3.forward),
                2f
                );
        _rotateAnim.SetOnComplete(() =>
        {
            RotateRecursively();
        });
        _rotateAnim.Start();
    }

    void HandleRouteRecursively()
    {
        var piece = _route.pieces[_routeIndex];
        var dur = _data.duration / _route.pieces.Count;
        transform.position = piece.Start;
        if (piece.RouteType == BaloonRouteType.Linear)
        {
            _moveAnim = transform.MoveAnim(piece.End, dur);
            _routeIndex = (_routeIndex + 1) % _route.pieces.Count;

            _moveAnim.SetOnComplete(() =>
            {
                HandleRouteRecursively();
            });
            _moveAnim.Start();
        }
        else
        {
            _moveAnim = transform.MoveAnimBezier(piece.End, piece.MiddlePoint, dur);

            _routeIndex = (_routeIndex + 1) % _route.pieces.Count;

            _moveAnim.SetOnComplete(() =>
            {
                HandleRouteRecursively();
            });
            _moveAnim.Start();

        }
    }
}
