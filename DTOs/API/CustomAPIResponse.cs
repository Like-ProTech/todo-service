namespace Todo_Service.DTOs.API
{
    public class CustomAPIResponse<T>
    {
        public int Code { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

    }
}
