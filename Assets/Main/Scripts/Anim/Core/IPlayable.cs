namespace Anim
{
    public interface IPlayable
    {
        public float Duration { get; }
        public float Delay { get; }
        public bool IsAnimating { get; }
        public bool IsFinished {get;}
        public int Id { get; }

        public void Start();
        public void Step(float delta);
        public void Stop();
        public void Destroy();
    }
}
