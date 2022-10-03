namespace TNS_store2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }

        ////

        public int? SubCategoryId { get; set; }

    }
}
