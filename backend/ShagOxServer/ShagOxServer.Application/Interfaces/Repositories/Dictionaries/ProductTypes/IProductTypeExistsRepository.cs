namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
public interface IProductTypeExistsRepository
{
    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByNameAsync(string name);
}