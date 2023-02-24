namespace CoNET.Repositories;

public interface IRepository<T>
    where T : class, IEntity, new()
{
    public IEnumerable<T> GetAll();
    public T? GetById(int id);
    public void Insert(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public bool ValidateInsert(T entity, IList<string> errors) => true;
    public bool ValidateUpdate(T entity, IList<string> errors) => true;
}