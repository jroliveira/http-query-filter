namespace Http.Query.Filter.Client.Test
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class AccountsClient
    {
        public Filter<IEnumerable<dynamic>> GetAll()
        {
            return new Filter<IEnumerable<dynamic>>(async queryFilter => await Task.Run(() => new List<dynamic>()));
        }
    }
}