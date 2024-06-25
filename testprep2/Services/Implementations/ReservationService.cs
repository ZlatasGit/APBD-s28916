namespace Services.Implementations
{
    using DTOs;
    using Context;
    using Services.Interfaces;
    using Models;

    public class ReservationService : IReservationService
    {
        private readonly BoatDbContext _context;

        public ReservationService(BoatDbContext context)
        {
            _context = context;
        }

        private ReservationGetDTO Map(Reservation reservation)
        {
            return new ReservationGetDTO
            {
                // Map the properties of the Reservation object to ReservationGetDTO
                // Implement the mapping logic here
            };
        }

        public IEnumerable<ReservationGetDTO> GetReservationsByClientId(int clientId)
        {
            var reservations = _context.Reservations.Where(r => r.ClientId == clientId);
            var reservationGetDTOs = new List<ReservationGetDTO>();

            foreach (var reservation in reservations)
            {
                var reservationGetDTO = Map(reservation);
                reservationGetDTOs.Add(reservationGetDTO);
            }

            return reservationGetDTOs;
        }

        public IEnumerable<ReservationGetDTO> GetAllReservations()
        {
            var reservations = _context.Reservations;
            var reservationGetDTOs = new List<ReservationGetDTO>();

            foreach (var reservation in reservations)
            {
                var reservationGetDTO = Map(reservation);
                reservationGetDTOs.Add(reservationGetDTO);
            }

            return reservationGetDTOs;
        }

        public ReservationGetDTO GetReservationById(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.IdReservation == id);
            return Map(reservation);
        }

        public ReservationGetDTO AddReservation(ReservationPostDTO reservationPostDTO)
        {
            var reservation = new Reservation
            {
                // Map the properties of ReservationPostDTO to Reservation
                // Implement the mapping logic here
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return Map(reservation);
        }

        public ReservationGetDTO UpdateReservation(int id, ReservationPostDTO reservationPutDTO)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.IdReservation == id);

            // Update the properties of the reservation object using the properties of reservationPutDTO
            // Implement the update logic here

            _context.SaveChanges();

            return Map(reservation);
        }

        public ReservationGetDTO DeleteReservation(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.IdReservation == id);
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return Map(reservation);
        }
    }
}