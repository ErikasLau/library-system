namespace API_library_system.DTO
{
	public class ReservationPriceInputDTO
	{
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public bool IsQuickPickup { get; set; }
		public int BookId { get; set; }
	}
}
