using API_library_system.Models;

namespace API_library_system.Services
{
	public class ReservationServices
	{
		private decimal serviceFee = 3;
		private decimal quickPickupFee = 5;

		public ReservationPrice CalculateReservationPrice(LibraryItem book, DateTime fromDate, DateTime toDate, bool isQuickPickup)
		{
			decimal sum = 0;
			decimal discount = 0;

			int days = (int)Math.Ceiling((toDate - fromDate).TotalDays);

			decimal discountRate = (decimal)(days > 3 && days <= 10 ? 0.1 : days > 10 ? 0.2 : 0);
			discount = days * book.Price * discountRate;
			sum = days * book.Price * (1 - discountRate) + serviceFee + (isQuickPickup ? quickPickupFee : 0);

			return new ReservationPrice
			{
				TotalSum = sum,
				DiscountSum = discount,
				TotalDays = days,
				ServiceFee = serviceFee,
				QuickPickupFee = isQuickPickup ? quickPickupFee : 0
			};
		}
	}
}
