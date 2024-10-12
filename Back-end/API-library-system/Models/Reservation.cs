namespace API_library_system.Models
{
	public class Reservation
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public bool IsQuickPickUp { get; set; }
		public LibraryItem SelectedItem { get; set; }

		public Reservation(int id, DateTime createdAt, DateTime fromDate, DateTime toDate, bool isQuickPickUp)
		{
			Id = id;
			CreatedAt = createdAt;
			FromDate = fromDate;
			ToDate = toDate;
			IsQuickPickUp = isQuickPickUp;
		}

		public void SetSelectedItem(LibraryItem selectedItem)
		{
			SelectedItem = selectedItem;
		}
	}
}
