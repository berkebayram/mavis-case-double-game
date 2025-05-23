public class State : IState
{
    public float DropCooldown { get; private set; }
    public int HeartCount { get; private set; }

    public State(float cd, int hc)
    { 
        DropCooldown = cd;
        HeartCount = hc;
    }
}
