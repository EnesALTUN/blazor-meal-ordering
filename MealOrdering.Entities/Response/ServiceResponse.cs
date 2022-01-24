namespace MealOrdering.Entities.Response
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
