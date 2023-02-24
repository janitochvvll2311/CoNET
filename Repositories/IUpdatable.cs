namespace CoNET.Repositories;

public interface IUpdatable<T>
{
    public void Update(T data);
}