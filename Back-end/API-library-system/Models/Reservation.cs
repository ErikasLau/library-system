﻿namespace API_library_system.Models
{
	public class Reservation
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; } = DateTime.Now;
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public bool IsQuickPickUp { get; set; }
		public ReservationPrice? TotalPrice { get; set; }
		public LibraryItem? Book { get; set; }
		public int BookId { get; set; }
		public Reservation(DateTime fromDate, DateTime toDate, bool isQuickPickUp, int bookId)
		{
			FromDate = fromDate;
			ToDate = toDate;
			IsQuickPickUp = isQuickPickUp;
			BookId = bookId;
		}
	}
}
