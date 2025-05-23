public class CutSceneState : IState
{
    public float DropCooldown {get;private set;}
    public int HeartCount { get; private set; }
    public int SceneIndex{get;private set;}
    public CutSceneState(float cd, int hc, int index)
    {
        DropCooldown = cd;
        HeartCount = hc;
        SceneIndex = index;
    }
}
