namespace HamsterApi.Domain.RepositoriesInterfaces.Base;

public interface IBaseRepository<T>
{
    public Task<string> Create(T item);

    public Task<T?> Read(string id);

    public Task<List<T>> ReadAll();

    public Task<bool> Delete(string id);

    public Task<List<T>> ReadByIds(IEnumerable<string> ids);
}
