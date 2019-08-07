namespace Http.Query.Filter.Client.Builders
{
    using System.Threading.Tasks;

    public interface IBuilder<TReturn>
    {
        Task<TReturn> Build();
    }
}
