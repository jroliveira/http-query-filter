namespace Http.Query.Filter.Integration.Test.Infrastructure.Data.Linq.Collections
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Http.Query.Filter.Integration.Test.Entities;

    internal sealed class Accounts : ReadOnlyCollection<Account>
    {
        private static readonly IList<Account> DefaultData = new List<Account>
        {
            new Account(01, "junoliv.e@gmail.com", "123456"),
            new Account(02, "junoli.ve@gmail.com", "654321"),
            new Account(03, "junol.ive@gmail.com", "111111"),
            new Account(04, "jun.olive@gmail.com", "222222"),
            new Account(05, "j.unolive@gmail.com", "333333"),
            new Account(06, "ju.nolive@gmail.com", "444444"),
            new Account(07, "jun.olive@gmail.com", "555555"),
            new Account(08, "juno.live@gmail.com", "666666"),
            new Account(09, "junol.ive@gmail.com", "777777"),
            new Account(10, "junoli.ve@gmail.com", "888888"),
            new Account(11, "junoliv.e@gmail.com", "999999"),
        };

        internal Accounts()
            : base(DefaultData)
        {
        }
    }
}
