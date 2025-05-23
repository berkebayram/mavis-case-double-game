using Main.Containers;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;
using UnityEngine;
using Zenject;

public class Trolley : MonoBehaviour
{
    [SerializeField] private float tiltPower = 0.01f;
    [Inject] private ProductFallManager _fallManager;
    [Inject] private GameLevel _level;
    private float _y;
    private bool _isActive;
    Camera cam;

    void OnEnable()
    {
        Dispatcher.Subscribe<GameRestartEvent>(HandleRestart);
        Dispatcher.Subscribe<GameStartEvent>(HandleStart);
        Dispatcher.Subscribe<GameFailEvent>(HandleStop);
    }

    void OnDisable()
    {
        Dispatcher.Unsubscribe<GameRestartEvent>(HandleRestart);
        Dispatcher.Unsubscribe<GameStartEvent>(HandleStart);
        Dispatcher.Unsubscribe<GameFailEvent>(HandleStop);
    }

    private void HandleStop(GameFailEvent @event)
    {
        _isActive = false;
    }

    private void HandleStart(GameStartEvent @event)
    {
        _isActive = true;
    }

    private void HandleRestart(GameRestartEvent @event)
    {
        var pos = transform.position;
        pos.x = 0f;
        transform.position = pos;
    }

    void Start()
    {
        cam = Camera.main;
        _y = transform.position.y;
    }

    void Update()
    {
        AdjustTilt(0);
        if (!HasInput())
            return;


        var inp = GetInput();

        var pos = cam.ScreenToWorldPoint(inp);
        pos.y = _y;
        pos.z = 0;
        pos.x = Mathf.Clamp(pos.x, -1.6f, 1.6f);

        var diff = pos.x - transform.position.x;
        if (diff != 0)
            AdjustTilt(diff > 0 ? tiltPower : -tiltPower);

        transform.position = pos;
    }

    void AdjustTilt(float val)
    {
        var rot = Quaternion.AngleAxis(val, Vector3.forward);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rot, Time.deltaTime * 5f);
    }

    Vector2 GetInput()
    {
        if (Input.touchCount > 0)
            return Input.GetTouch(0).position;
        if (Input.GetMouseButton(0))
            return Input.mousePosition;
        return Vector2.zero;
    }

    bool HasInput()
    {
        return (Input.touchCount > 0 || Input.GetMouseButton(0)) && _isActive;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        var rb = col.attachedRigidbody;
        if (!rb)
            return;

        var p = rb.GetComponent<Product>();
        if (!p || p.IsDestroying)
            return;
        p.TransformApart(_fallManager, _level);
    }
}
