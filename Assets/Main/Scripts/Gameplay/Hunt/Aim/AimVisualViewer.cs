using UnityEngine;
using Anim.UnityBindings;
using Anim.Ease;

public class AimVisualViewer : MonoBehaviour
{
    [SerializeField] private GameObject altModel;
    [SerializeField] private SpriteRenderer rend;

    [SerializeField] private float animSpeed;
    private bool _tester;
    private int _animId = -1;
    private int _scaleAnimId = -1;

    public void SetTarget(bool inDanger)
    {
        rend.color = inDanger ? Color.red : Color.white;
        altModel.SetActive(inDanger);

        if (_animId != -1)
        {
            TweenRunner.Instance.Destroy(_animId);
            _animId = -1;
            _scaleAnimId = -1;
            transform.localScale = Vector3.one * .2f;
            transform.rotation = Quaternion.identity;
        }

        if (inDanger)
        {
            AnimateRecursively();
        }
    }

    void AnimateRecursively()
    {
        var anim = transform.RotateAnim(
                Quaternion.AngleAxis(animSpeed, Vector3.forward) * transform.rotation,
                2f);
        _animId = anim.Id;
        anim.SetOnComplete(() =>
        {
            AnimateRecursively();
        });
        anim.Start();

        var scaleAnim = transform.ScaleAnim(Vector3.one * .24f, .5f, 0f, new OutQuadEaser());
        scaleAnim.SetOnComplete(() =>
        {
            var s = transform.ScaleAnim(Vector3.one * .2f, .5f, 0f, new InQuadEaser());
            _scaleAnimId = s.Id;
            s.Start();
        });
        _scaleAnimId = scaleAnim.Id;
        scaleAnim.Start();
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetTarget(_tester);
            _tester = !_tester;
        }
    }
}

