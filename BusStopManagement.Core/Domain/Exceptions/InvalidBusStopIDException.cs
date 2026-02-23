namespace BusStopManagement.Core.Domain.Exceptions
{
    public class InvalidBusStopIDException : ArgumentException
    {
        public InvalidBusStopIDException() : base()
        {
            
        }

        public InvalidBusStopIDException(string? message) : base(message)
        {
            
        }

        public InvalidBusStopIDException(string? message, Exception? innerException) : base(message, innerException)
        {
            
        }
    }
}
