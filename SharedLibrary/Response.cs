using SharedLibrary.Dtos;
using System.Text.Json.Serialization;

namespace SharedLibrary
{
    public class Response<T> where T : class
    {
        public T Data { get; private set; }
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccess { get; private set; }
        public ErrorDto Error { get; private set; }


        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccess = true };
        }
        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default, IsSuccess = true, StatusCode = statusCode };
        }
        public static Response<T> Fail(ErrorDto error, int statusCode)
        {
            return new Response<T> { Error = error, IsSuccess = false, StatusCode = statusCode };
        }
        public static Response<T> Fail(string errorMessage, int statusCode, bool isShow)
        {
            var errorDto = new ErrorDto(errorMessage, isShow);

            return new Response<T> { Error = errorDto, IsSuccess = false, StatusCode = statusCode };
        }
    }
}
