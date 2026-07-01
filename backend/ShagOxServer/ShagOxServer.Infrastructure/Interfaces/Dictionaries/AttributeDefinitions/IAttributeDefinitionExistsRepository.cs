namespace ShagOxServer.Infrastructure.Interfaces.Dictionaries.AttributeDefinitions;
public interface IAttributeDefinitionExistsRepository
{
    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByCategoryAsync(int attributeId, int categoryId);
}
