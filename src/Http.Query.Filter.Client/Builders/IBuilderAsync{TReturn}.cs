namespace Http.Query.Filter.Client.Builders
{
    using System.Threading.Tasks;

    public interface IBuilderAsync<TReturn>
    {
        Task<TReturn> BuildAsync();
    }
}