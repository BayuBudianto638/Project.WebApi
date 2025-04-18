namespace Project.WebApi.Entities.Models
{
    public class ResponseGetBase<T>
    {
        public string Message { get; set; } = string.Empty;
        public string TransactionId { get; set; } = Guid.NewGuid().ToString();
        public T? Data { get; set; }

        public ResponseGetBase() { }

        public ResponseGetBase(string message, T? data)
        {
            Message = message;
            Data = data;
        }

        public static ResponseGetBase<T> Success(T data, string message = "Success")
        {
            return new ResponseGetBase<T>(message, data);
        }

        public static ResponseGetBase<T> Fail(string message)
        {
            return new ResponseGetBase<T>(message, default);
        }
    }

}
