namespace Domain.Response
{
    public interface IBaseResponse<T>
    {
        string Description { get; }

        ResponseCode StatusCode { get; }

        T Data { get; }
    }
}
