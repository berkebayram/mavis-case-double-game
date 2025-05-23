using UnityEngine;
using TMPro;
using Anim.UnityBindings;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gui;
    [SerializeField] private CanvasGroup group;

    public void Setup(string context)
    {
        gui.SetText(context);
        var move = transform.MoveAnim(
                transform.position + Vector3.up,
                10f
                );
        move.Start();

        var fade = group.FadeAnim(0f, 1f);
        fade.SetOnComplete(() =>
        {
            TweenRunner.Instance.Destroy(move.Id);
            TweenRunner.Instance.Destroy(fade.Id);
            Destroy(gameObject);
        });
        fade.Start();

    }
}
