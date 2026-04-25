using ProductCatalog.Api.DTOs.Category;
using ProductCatalog.Api.DTOs.Product;
using ProductCatalog.Api.Entities;

public class CategoryService
{
    private readonly IRepository<Category> _repository;

    public CategoryService(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();

        return categories.Select(c => new CategoryResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Products = c.Products.Select(p => new ProductSummaryDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList()
        }).ToList();
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Products = category.Products.Select(p => new ProductSummaryDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList()
        };
    }


    public async Task CreateAsync(CategoryRequestDto dto)
    {
        var Category = new Category
        {
            Name = dto.Name,
        };

        await _repository.AddAsync(Category);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, CategoryRequestDto dto)
    {
        var Category = await _repository.GetByIdAsync(id);

        if (Category == null)
            throw new Exception("Category not found");

        Category.Name = dto.Name;
        Category.UpdatedAt = DateTime.UtcNow;

        _repository.Update(Category);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var Category = await _repository.GetByIdAsync(id);

        if (Category == null)
            throw new Exception("Category not found");

        Category.Active = false;
        Category.UpdatedAt = DateTime.UtcNow;

        _repository.Update(Category);
        await _repository.SaveChangesAsync();
    }
}