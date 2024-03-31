namespace Api.Data.Tests.Helpers
{
    public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _innerEnumerator;

        public TestAsyncEnumerator(IEnumerator<T> innerEnumerator)
        {
            _innerEnumerator = innerEnumerator ?? throw new ArgumentNullException(nameof(innerEnumerator));
        }

        public T Current => _innerEnumerator.Current;

        public async ValueTask<bool> MoveNextAsync()
        {
            return await Task.FromResult(_innerEnumerator.MoveNext());
        }

        public ValueTask DisposeAsync()
        {
            _innerEnumerator.Dispose();
            return ValueTask.CompletedTask;
        }
    }    
}
