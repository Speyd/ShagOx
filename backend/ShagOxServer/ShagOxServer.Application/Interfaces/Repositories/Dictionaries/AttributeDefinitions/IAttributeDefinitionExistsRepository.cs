namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions;
public interface IAttributeDefinitionExistsRepository
{
    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByCategoryAsync(int attributeId, int categoryId);

    Task<bool> ExistsByCategoryAsync(string attributeKey, int categoryId);
}