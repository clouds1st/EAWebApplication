namespace EA_API
{
    public class EA_ProductsInMemory

    {
        public int Id { get; set; }         // Primary key, auto-generated
        public string Name { get; set; }    // Required
        public decimal Price { get; set; }  // Required
        public string Productdesc { get; set; } // Optional
        public DateTime CreatedDate { get; set; } // Required
    }
}
