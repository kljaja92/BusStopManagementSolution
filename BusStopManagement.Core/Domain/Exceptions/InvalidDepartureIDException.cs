namespace BusStopManagement.Core.Domain.Exceptions
{
    internal class InvalidDepartureIDException : ArgumentException
    {
        public InvalidDepartureIDException() : base()
        {
            
        }

        public InvalidDepartureIDException(string? message) : base(message)
        {
            
        }

        public InvalidDepartureIDException(string? message, Exception? innerException) : base(message, innerException)
        {
            
        }
    }
}
