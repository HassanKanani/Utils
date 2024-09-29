namespace Utils.Common
{
    public interface IEntity{}
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<int> { }
}
