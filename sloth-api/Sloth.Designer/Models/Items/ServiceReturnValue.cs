namespace Sloth.Designer.Models;

public class ServiceReturnValue<T>
{
    public T? Data { get; set; }
    public int ResponseCode { get; set; }
    public string? Error { get; set; }
    public bool Success { get; set; }
}
