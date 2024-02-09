namespace Domain.Response
{
    public class Response<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public ResponseCode StatusCode { get; set; }
        public T Data { get; set; }
    }
}
