using UnityEngine;

public class FtueHand : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset;

    public void Follow(Transform t)
    {
        _target = t;
        _offset = _target.position - transform.position;
        gameObject.SetActive(true);
    }

    public void Unfollow()
    {
        _target = null;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!_target)
            return;

        transform.position = _target.position - _offset;
    }
}
