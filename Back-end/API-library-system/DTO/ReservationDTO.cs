namespace API_library_system.DTO
{
	public class ReservationDTO
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public bool IsQuickPickUp { get; set; }
		public ReservationPriceDTO? TotalPrice { get; set; }
		public LibraryItemDTO? Book { get; set; }
	}
}
