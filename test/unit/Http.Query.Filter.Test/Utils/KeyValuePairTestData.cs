namespace Http.Query.Filter.Test.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public abstract class KeyValuePairTestData<TValue, TReturn> : IEnumerable<object[]>
        where TReturn : ReadOnlyCollection<KeyValuePair<string, TValue>>
    {
        protected static readonly Func<string, TValue, KeyValuePair<string, TValue>> Item = (key, value) => new KeyValuePair<string, TValue>(key, value);
        protected static readonly Func<string, TValue, TReturn> Field = (key, value) => Fields(data => data.Add(Item(key, value)));
        protected static readonly Func<Action<IList<KeyValuePair<string, TValue>>>, TReturn> Fields = afterCreating =>
        {
            var data = new List<KeyValuePair<string, TValue>>();
            afterCreating(data);

            var args = new object[] { data };
            var @return = Activator.CreateInstance(typeof(TReturn), args) as TReturn;

            return @return;
        };

        protected abstract List<object[]> Data { get; }

        public IEnumerator<object[]> GetEnumerator()
        {
            return this.Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}