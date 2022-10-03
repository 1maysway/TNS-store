namespace TNS_store2.Models
{
    public class Fabricator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public string Country { get; set; }
    }
}
