namespace Anim.Binder
{
    public interface IAnimBinder<V>
    {
        public V Get();
        public void Set(V val);
    }
}
