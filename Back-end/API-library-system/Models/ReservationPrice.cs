namespace API_library_system.Models
{
	public class ReservationPrice
	{
		public int Id { get; set; }
		public decimal TotalSum { get; set; }
		public decimal DiscountSum { get; set; }
		public int TotalDays { get; set; }
		public decimal ServiceFee { get; set; }
		public decimal QuickPickupFee { get; set; }
	}
}
