using System.Collections;

public class ShelfAnimator 
{
    public bool IsAnimating { get; private set; }
    public ShelfAnimationContent Content;

    public ShelfAnimator(ShelfAnimationContent content)
    {
        Content = content;
    }

    IEnumerator Animate(ShelfAnimationContent content)
    {
        if (content == null)
            yield break;

        yield return null;
    }

    public void StartAnimation(ShelfAnimationContent content)
    {
        ShelfAnimationContent animContent;

        if (content != null && content is ShelfAnimationContent ct)
            animContent = ct;
        else
            animContent = Content;
    }


    public void StopAnimation()
    {
        throw new System.NotImplementedException();
    }
}
