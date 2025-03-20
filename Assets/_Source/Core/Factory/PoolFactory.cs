namespace Core.Factory
{
    public abstract class PoolFactory<T> : Factory<T>
    {
        public abstract void Release(T projectile);
    }
}