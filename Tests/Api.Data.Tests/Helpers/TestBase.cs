namespace Api.Data.Tests.Helpers
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Moq.AutoMock;

    public abstract class TestBase
    {
        protected AutoMocker Mocker;

        public TestBase()
        {
            Mocker = new AutoMocker();
        }

        protected DbSet<T> GetMockDbSet<T>(List<T> list)
            where T : class
        {
            var query = list.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();

            mockDbSet.As<IAsyncEnumerable<T>>()
                .Setup(x => x.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(query.GetEnumerator()));

            mockDbSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<T>(query.Provider));

            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(query.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(query.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(query.GetEnumerator());

            return mockDbSet.Object;
        }
    }
}
