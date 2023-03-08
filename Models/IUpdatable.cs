namespace CoNET.Models;

public interface IUpdatable<T>
{
    public void Update(T data);
}