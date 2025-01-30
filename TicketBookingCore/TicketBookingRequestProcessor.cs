
using Moq;

namespace TicketBookingCore
{
    public class TicketBookingRequestProcessor
    {
        public interface ITicketBookingRepository
        {

        }

        public void Save(TicketBookingRequest request)
        {
            TicketBooking ticketBooking = new TicketBooking
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };

        }

        public TicketBookingRequestProcessor()
        {
            Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;
        }

        public TicketBookingResponse Book(TicketBookingRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return new TicketBookingResponse
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };
        }
    }
}