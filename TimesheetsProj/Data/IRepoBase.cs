namespace TimesheetsProj.Data
{
    public interface IRepoBase<T>
    {
        Task<T> GetItem(Guid id);
        Task<IEnumerable<T>> GetItems();
        Task<int> Add(T item);
        Task<int> Update(Guid id, T item);
    }
}
