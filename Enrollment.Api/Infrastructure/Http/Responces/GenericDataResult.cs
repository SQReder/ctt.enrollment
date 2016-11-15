namespace Enrollment.Api.Infrastructure.Http.Responces
{
    public class GenericDataResult<T> : SuccessResult
    {
        public T Data { get; set; }

        public GenericDataResult(T data)
        {
            Data = data;
        }
    }
}