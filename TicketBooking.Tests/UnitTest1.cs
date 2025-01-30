using Moq;
using static TicketBookingCore.TicketBookingRequestProcessor;

namespace TicketBookingCore.Tests
{
    public class TicketBookingRequestProcessorTests
    {
        private readonly TicketBookingRequestProcessor _processor;
        private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;

        public TicketBookingRequestProcessorTests()
        {
            _ticketBookingRepositoryMock = new Mock<ITicketBookingRepository>();
            _processor = new TicketBookingRequestProcessor(_ticketBookingRepositoryMock.Object);
        }

        [Fact]
        public void ShouldReturnTicketBookingResultWithRequestValues()
        {

            var request = new TicketBookingRequest
            {
                FirstName = "Martin",
                LastName = "Grasevski",
                Email = "Martin.Grasevski@hotmail.com"
            };

            TicketBookingResponse response = _processor.Book(request);

            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);

        }
        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            var act = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));
            Assert.Equal("request", act.ParamName);
        }
        [Fact]
        public void ShouldSaveToDatabase()
        {
            var request = new TicketBookingRequest
            {
                FirstName = "Martin",
                LastName = "Grasevski",
                Email = "Martin.Grasevski@hotmail.com"
            };

            _processor.Save(request);

            _ticketBookingRepositoryMock.Verify(repo => repo.Save(It.Is<TicketBookingRequest>(r =>
            r.FirstName == request.FirstName &&
            r.LastName == request.LastName &&
            r.Email == request.Email
            )), Times.Once());
        }
    }
}
