using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(int id);

    void Add(Category category);

    bool Update(Category category);

    void Delete(Category category);
}