using ProductCatalog.Api.Entities;
using Microsoft.EntityFrameworkCore;

public class CategoryRepository : IRepository<Category>
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await _context.Categories.Include(x => x.Products).ToListAsync();

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Category category)
        => await _context.Categories.AddAsync(category);

    public void Update(Category category)
        => _context.Categories.Update(category);

    public void Delete(Category category)
        => _context.Categories.Remove(category);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public IQueryable<Category> Query()
    {
        throw new NotImplementedException();
    }
}