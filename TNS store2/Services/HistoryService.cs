using System.Linq;
using System.Text.Json;
using TNS_store2.Models;

namespace TNS_store2.Services
{
    public class HistoryService : IHistoryService
    {

        private readonly TnsDbContext context;
        public HistoryService(TnsDbContext context)
        {
            this.context = context;
        }
        public void Add(HttpContext httpContext, int id)
        {
            List<int> history;
            if (!httpContext.Session.Keys.Contains("history"))
            {
                history = new List<int>();
                history.Add(id);

            }
            else
            {
                history = JsonSerializer.Deserialize<List<int>>(httpContext.Session.GetString("history"));

                
                if (!history.Contains(id))
                {
                    history.Insert(0, id);
                }
                if (history.Count > 4)
                {
                    history.RemoveAt(history.Count-1);
                }

            }
            httpContext.Session.SetString("history", JsonSerializer.Serialize(history));
        }

        public void CLear(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts(HttpContext httpContext)
        {
            IEnumerable<Product> products = null;
            if (httpContext.Session.Keys.Contains("history"))
            {
                var history = JsonSerializer.Deserialize<List<int>>(httpContext.Session.GetString("history"));
                

                if (history.Count > 0)
                {
                    products = context.Products.ToList().Where(p => history.Contains(p.Id));
                    var sortedProducts = products.ToArray();
                    Array.Sort(sortedProducts, (x, y) => history.IndexOf(x.Id) - history.IndexOf(y.Id));
                    return sortedProducts;
                }
            }
            return null;
        }

        public void Remove(HttpContext httpContext, int id)
        {
            if (httpContext.Session.Keys.Contains("history"))
            {
                var history = JsonSerializer.Deserialize<List<int>>(httpContext.Session.GetString("history"));
                history.Remove(id);
                httpContext.Session.SetString("history", JsonSerializer.Serialize(history));
            }
        }
    }
}
