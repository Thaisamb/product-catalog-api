using ProductCatalog.Api.DTOs.Product;

namespace ProductCatalog.Api.DTOs.Category
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }= new string(string.Empty);

        public List<ProductSummaryDto> Products { get; set; } = new();
    }
}
