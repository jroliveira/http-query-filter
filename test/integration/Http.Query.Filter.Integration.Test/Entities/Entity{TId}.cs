namespace Http.Query.Filter.Integration.Test.Entities
{
    internal class Entity<TId>
    {
        public Entity(TId id) => this.Id = id;

        public TId Id { get; }
    }
}
