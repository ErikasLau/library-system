export default function Reservation({ reservation }) {
  console.log(reservation);

  const formatDate = (date) => date.split("T")[0];

  const formattedCreatedAt = formatDate(reservation.createdAt);
  const formattedFromDate = formatDate(reservation.fromDate);
  const formattedToDate = formatDate(reservation.toDate);

  return (
    <div className="reservation flex p-2 border border-black rounded-md">
      <div className="basis-1/4">
        <img
          className="reservation-image max-h-24 max-w-24 object-cover m-auto"
          src={`data:image/jpeg;base64,${reservation.book.pictureData}`}
          alt={reservation.book.name}
        />
      </div>
      <div className="basis-1/4 flex flex-col justify-center">
        <div className="font-bold text-lg">{reservation.book.name}</div>
        <div className="text-sm">
          <span className="text-gray-500">{formattedCreatedAt}</span> ·{" "}
          {reservation.book.bookType}
        </div>
      </div>
      <div className="basis-1/4 flex flex-col justify-center">
        <div className="reservation-details flex flex-col gap-1">
          <div className="flex items-center flex-col">
            <span>
              <strong>From:</strong> {formattedFromDate}
            </span>
            <span>
              <strong>To:</strong> {formattedToDate}
            </span>
          </div>
          {reservation.isQuickPickUp && (
            <span className="mr-2 bg-yellow-200 text-yellow-700 px-1 py-0.5 rounded-full text-center">
              Quick Pickup
            </span>
          )}
        </div>
      </div>
      <div className="font-bold basis-1/4 flex flex-col justify-center text-center">
        Total: {reservation.totalPrice.totalSum}€
      </div>
    </div>
  );
}
