namespace MultiShop.Mvc.Models.ViewModels
{
    public class ProductDto
    {
        public ProductDto()
        {
            Count = 1;
        }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
