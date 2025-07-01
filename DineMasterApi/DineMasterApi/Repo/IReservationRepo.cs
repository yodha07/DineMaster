using DineMasterApi.DTO;

namespace DineMasterApi.Repo
{
    public interface IReservationRepo
    {
        Task AddReservationAsync(ReservationDTO1 reservation);
        Task<List<ReservationDTO2>> GetAllAsync();
        Task<ReservationDTO3> GetReservationByIdAsync(int id);
        Task UpdateReservationAsync(ReservationDTO3 reservation);
        Task<int> DeleteReservationAsync(int id);
        Task<List<TableDTO2>> GetAvailableTableAsync();
        Task<List<TableDTO2>> GetSuitableTableAsync(ReservationDTO4 reservation);
        Task<bool> CheckInAsync(int id);
        Task<bool> CheckOutAsync(int id);
    }
}
