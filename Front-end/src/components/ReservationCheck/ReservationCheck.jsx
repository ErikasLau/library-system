export default function ReservationCheck({ reservationDetails }) {
    let currencyVal = "€";
  return (
    <div className="reservation-check border border-gray-300 p-2 rounded-md">
      <h3 className="text-center font-bold">Reservation Details</h3>
      <ul>
        <li className="text-sm">
          <span className="font-semibold">Total days:</span>{" "}
          {reservationDetails.totalDays}
        </li>
        <li className="text-sm">
          <span className="font-semibold">Service fee:</span>{" "}
          {reservationDetails.serviceFee}{currencyVal}
        </li>
        <li className="text-sm">
          <span className="font-semibold">Quick pickup fee:</span>{" "}
          {reservationDetails.quickPickupFee}{currencyVal}
        </li>
        <li className="text-sm">
          <span className="font-semibold">Discount:</span>{" "}
          {reservationDetails.discountSum}{currencyVal}
        </li>
        <li className="text-lg">
          <span className="font-semibold">Total sum:</span>{" "}
          {reservationDetails.totalSum}{currencyVal}
        </li>
      </ul>
    </div>
  );
}
