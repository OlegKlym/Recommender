namespace Recommender.Core.Models
{
    public class ResponseData<T> : IResponseData<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public bool IsSuccessful => StatusCode == 200;

        public ResponseData()
        {
            // empty
        }

        public ResponseData(T data)
        {
            Data = data;
        }
    }

    public interface IResponseData<T>
    {
        T Data { get; set; }
        int StatusCode { get; set; }
        string ErrorMessage { get; set; }

        bool IsSuccessful { get; }
    }
}
