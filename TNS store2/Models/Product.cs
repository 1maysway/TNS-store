namespace TNS_store2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPreferredProduct { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        //public Category Category { get; set; }
        public IEnumerable<OrderProduct> OrderProducts { get; set; }

        ////

        public int  FabricatorId { get; set; }
    }
}
