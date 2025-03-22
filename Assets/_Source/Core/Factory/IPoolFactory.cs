namespace Core.Factory
{
    public interface IPoolFactory<T> : IFactory<T>
    {
        void Release(T projectile);
    }
}