using TNS_store2.Models;

namespace TNS_store2.Services
{
    public interface ICartService
    {
        void Add(HttpContext httpContext, int id);

        void Remove(HttpContext httpContext,int id);

        //IEnumerable<Product> GetProducts();

        //IEnumerable<int> GetProductIds();

        IEnumerable<CartItemViewModel> GetProducts(HttpContext httpContext);

        void Clear(HttpContext httpContext);
        void Decrement(HttpContext httpContext, int id);
    }
}
