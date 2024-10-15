namespace API_library_system.DTO
{
    public class ReservationInputDTO
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsQuickPickUp { get; set; }
        public int BookId { get; set; }
    }
}
