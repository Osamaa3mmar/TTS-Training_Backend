namespace Backend.PL
{
    public class ProductFileRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }
    }
}
