namespace Wolfpack.Business.Models;

public interface IServiceResponse<T> : ISimpleServiceResponse
{
    public T? TargetObject { get; set; }
}