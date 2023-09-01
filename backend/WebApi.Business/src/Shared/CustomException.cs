namespace WebApi.Business.src.Shared
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public CustomException(int statuscode = 500, string message = "Internal Server Error")
        {
            StatusCode = statuscode;
            ErrorMessage = message;
        }

        public static CustomException NotFoundException(string message = "Item cannot be found")
        {
            return new CustomException(404, message);
        }
    }
}