using UnityEngine;
using Main.Scripts.Events;
using Main.Scripts.EventSystem;

public class Aim : MonoBehaviour , IAim
{
    [SerializeField] private AimVisualViewer viewer;
    [SerializeField] private FloatingText text;
    [SerializeField] private float magnifier;
    [SerializeField] private float sens;

    [SerializeField] private Vector2 minMaxX;
    [SerializeField] private Vector2 minMaxY;
    [SerializeField] private bool _isActive;
    private Baloon _triggered;

    public void SetInput(bool isActive)
    {
        _isActive = isActive;
    }

    void Update()
    {

        var inp = GetInput();
        ProcessInput(inp);
    }

    void ProcessInput(Vector2 inp)
    {
        if (!_isActive)
            return;

        var target = Vector3.Lerp(transform.position, transform.position + (Vector3)inp * magnifier, sens);
        target.x = Mathf.Clamp(target.x, minMaxX.x, minMaxX.y);
        target.y = Mathf.Clamp(target.y, minMaxY.x, minMaxY.y);

        transform.position = target;
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    Vector2 GetInput()

    {
        if (!Input.GetMouseButton(0))
            return Vector2.zero;

        Vector2 delta;
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            delta = touch.deltaPosition;
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            delta = new Vector2(mouseX, mouseY);
        }
        return delta;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        var rb = col.attachedRigidbody;
        if (!rb)
            return;

        var b = rb.GetComponent<Baloon>();
        if (!b)
            return;

        _triggered = b;
        viewer.SetTarget(true);
    }

    public void Shoot()
    {
        if (!_triggered)
            return;

        var go = Instantiate(text, _triggered.transform.position, Quaternion.identity);
        go.Setup($"+{_triggered.Value}");
        Dispatcher.Dispatch<ChangeMoneyEvent>(new ChangeMoneyEvent() { Increment = _triggered.Value });
        _triggered?.StopAnimation();
        _triggered?.Boom();
        // Destroy(_triggered.gameObject);
        _triggered = null;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        var rb = col.attachedRigidbody;
        if (!rb)
            return;

        var b = rb.GetComponent<Baloon>();
        if (!b)
            return;

        _triggered = null;
        viewer.SetTarget(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
