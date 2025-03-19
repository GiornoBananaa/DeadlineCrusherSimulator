namespace Core.Factory
{
    public abstract class Factory<T>
    {
        public abstract T Create();
    }
    
    public abstract class PoolFactory<T> : Factory<T>
    {
        public abstract void Release(T obj);
    }
}