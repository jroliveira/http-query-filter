namespace Http.Query.Filter.Integration.Test.Infraestructure.Data.Linq.Collections
{
    using System.Collections.ObjectModel;

    using Http.Query.Filter.Integration.Test.Entities;

    internal class Accounts : Collection<Account>
    {
        public Accounts()
        {
            this.Add(new Account(01, "junoliv.e@gmail.com", "123456"));
            this.Add(new Account(02, "junoli.ve@gmail.com", "654321"));
            this.Add(new Account(03, "junol.ive@gmail.com", "111111"));
            this.Add(new Account(04, "jun.olive@gmail.com", "222222"));
            this.Add(new Account(05, "j.unolive@gmail.com", "333333"));
            this.Add(new Account(06, "ju.nolive@gmail.com", "444444"));
            this.Add(new Account(07, "jun.olive@gmail.com", "555555"));
            this.Add(new Account(08, "juno.live@gmail.com", "666666"));
            this.Add(new Account(09, "junol.ive@gmail.com", "777777"));
            this.Add(new Account(10, "junoli.ve@gmail.com", "888888"));
            this.Add(new Account(11, "junoliv.e@gmail.com", "999999"));
        }

        public static Accounts Data => new Accounts();
    }
}
