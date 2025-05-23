namespace Anim.Calculator
{
    public interface IAnimCalculator<V>
    {
        public V Start { get;set; }
        public V End { get;set; }
        public V Calculate(float progression);
    }
}
