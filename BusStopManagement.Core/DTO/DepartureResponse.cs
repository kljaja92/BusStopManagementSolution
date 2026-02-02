namespace BusStopManagement.Core.DTO
{
    public class DepartureResponse
    {
        public Guid DepartureID { get; set; }

        public string Destination { get; set; } = string.Empty;

        public DateTime DateAndTimeOfDeparture { get; set; }

        public byte NumberOfSeats { get; set; }

        public Guid BusStopID { get; set; }

        public override bool Equals(object? obj) =>
            obj is DepartureResponse other &&
            DepartureID == other.DepartureID &&
            Destination == other.Destination &&
            DateAndTimeOfDeparture == other.DateAndTimeOfDeparture &&
            NumberOfSeats == other.NumberOfSeats &&
            BusStopID == other.BusStopID;

        public override int GetHashCode() =>
            HashCode.Combine(DepartureID, Destination, DateAndTimeOfDeparture, NumberOfSeats, BusStopID);

        public override string ToString() =>
            $"DepartureID: {DepartureID}, Destination: {Destination}, Date: {DateAndTimeOfDeparture}, Seats: {NumberOfSeats}, BusStopID: {BusStopID}";
    }
}
