namespace Http.Query.Filter.Test.Utils
{
    using System.Collections;
    using System.Collections.Generic;

    public abstract class TestDataBase : IEnumerable<object[]>
    {
        protected abstract List<object[]> Data { get; }

        public IEnumerator<object[]> GetEnumerator() => this.Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
