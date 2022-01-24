namespace MealOrdering.Entities.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }

        public bool Success { get; set; }

        public String Message { get; set; }

        public void SetException(Exception exception)
        {
            Success = false;
            Message = exception.Message;
        }
    }
}
