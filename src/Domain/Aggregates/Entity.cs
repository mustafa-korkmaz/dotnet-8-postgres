namespace Domain.Aggregates
{
    /// <summary>
    /// base entity abstraction with TKey typed primary key
    /// </summary>
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; private set; }
        public DateTimeOffset CreatedAt { get; protected set; }

        protected EntityBase()
        {
            Id = default!;
        }

        protected EntityBase(TKey id)
        {
            Id = id;
        }
    }

    public interface IEntity<out TKey>
    {
        /// <summary>
        /// Primary key for table
        /// </summary>
        TKey Id { get; }
        DateTimeOffset CreatedAt { get; }
    }

    /// <summary>
    /// indicates that entity has a softDeletable option which prevents hard deletion from Db
    /// </summary>
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; }
    }

    public class ListEntityResponse<TEntity> where TEntity : class
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TEntity> Items { get; set; } = new List<TEntity>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long RecordsTotal { get; set; }
    }

    public class ListEntityRequest
    {
        public bool IncludeRecordsTotal { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}