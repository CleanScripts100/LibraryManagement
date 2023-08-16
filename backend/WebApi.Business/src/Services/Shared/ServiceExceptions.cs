namespace WebApi.Business.src.Services.Shared
{
    public class ServiceExceptions
    {
        public class ServiceException : Exception
        {
            public int StatusCode { get; set; }
            public string? Message { get; set; }

            public static ServiceException NotFoundException(string message = "The entity is not found")
            {
                return new ServiceException
                {
                    StatusCode = 404,
                    Message = message
                };
            }

            public static ServiceException UnAuthenticatedException(string message = "Credentials are wrong")
            {
                return new ServiceException
                {
                    StatusCode = 401,
                    Message = message
                };
            }
        }
    }
}