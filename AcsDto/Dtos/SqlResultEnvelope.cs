namespace AcsDto.Dtos;

public class SqlResultEnvelope<T>
{
    public SqlResultEnvelope(int count, T data)
    {
        this.Count = count;
        this.Data = data;
    }
    public int Count { get; private set; }
    public T Data { get; private set; }
}