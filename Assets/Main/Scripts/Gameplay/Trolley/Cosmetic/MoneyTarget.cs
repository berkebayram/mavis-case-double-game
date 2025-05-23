using UnityEngine;

public class MoneyTarget : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    [SerializeField] private Camera cam;

    void Update()
    {
        // transform.position = cam.ScreenToWorldPoint(target.position);
    }
}
