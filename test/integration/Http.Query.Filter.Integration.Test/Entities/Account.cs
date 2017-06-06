namespace Http.Query.Filter.Integration.Test.Entities
{
    internal sealed class Account : Entity<int>
    {
        public Account(int id, string email, string password)
        {
            this.Id = id;
            this.Email = email;
            this.Password = password;
        }

        public string Email { get; }

        public string Password { get; }
    }
}
