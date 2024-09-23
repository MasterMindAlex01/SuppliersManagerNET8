using MongoDB.Driver;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Application.Extensions
{
    public static class MongoDbExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IFindFluent<T,T> source, int pageNumber, int pageSize) where T : class
        {
            if (source == null) throw new Exception();
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            long count = await source.CountDocumentsAsync();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            List<T> items = await source.Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }
    }
}
