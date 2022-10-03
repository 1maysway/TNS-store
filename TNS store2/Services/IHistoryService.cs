using TNS_store2.Models;

namespace TNS_store2.Services
{
    public interface IHistoryService
    {
        void Add(HttpContext httpContext, int id);

        void Remove(HttpContext httpContext, int id);

        //IEnumerable<Product> GetProducts();

        //IEnumerable<int> GetProductIds();

        IEnumerable<Product> GetProducts(HttpContext httpContext);

        void CLear(HttpContext httpContext);
    }
}
