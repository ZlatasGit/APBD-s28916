namespace Services.Interfaces;
using DTOs;

public interface IReservationService
{
    IEnumerable<ReservationGetDTO> GetAllReservations();
    ReservationGetDTO GetReservationById(int id);
    ReservationGetDTO AddReservation(ReservationPostDTO reservationPostDTO);
    ReservationGetDTO UpdateReservation(int id, ReservationPostDTO reservationPutDTO);
    ReservationGetDTO DeleteReservation(int id);
}