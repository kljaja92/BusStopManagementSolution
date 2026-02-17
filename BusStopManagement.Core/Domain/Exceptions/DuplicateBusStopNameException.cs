namespace BusStopManagement.Core.Domain.Exceptions
{
    public class DuplicateBusStopNameException : ArgumentException
    {
        public DuplicateBusStopNameException() : base()
        {
            
        }

        public DuplicateBusStopNameException(string? message) : base(message)
        {
            
        }

        public DuplicateBusStopNameException(string? message, Exception? innerException) : base(message, innerException)
        {
            
        }
    }
}
