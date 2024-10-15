namespace API_library_system.DTO
{
	public class ReservationPriceDTO
	{
		public decimal TotalSum { get; set; }
		public decimal DiscountSum { get; set; }
		public int TotalDays { get; set; }
		public decimal ServiceFee { get; set; }
		public decimal QuickPickupFee { get; set; }
	}
}
