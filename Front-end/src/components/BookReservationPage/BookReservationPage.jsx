import { useLoaderData } from "react-router-dom";
import DatePicker from "react-datepicker";
import { useState } from "react";
import "react-datepicker/dist/react-datepicker.css";
import {
  getReservationPrice,
  postReservation,
} from "../../api/ApiReservations";

export default function BookReservationPage() {
  let book = useLoaderData();

  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(null);
  const [reservationPrice, setReservationPrice] = useState(0);
  const [quickPickup, setQuickPickup] = useState(false);

  const onChange = (dates) => {
    const [start, end] = dates;
    setStartDate(start);
    setEndDate(end);

    if (end - start > 0) {
      getReservationPrice(start, end, quickPickup, book.id).then((res) => {
        setReservationPrice(res.data);
      });
    } else {
      setReservationPrice(0);
    }
  };

  const onReserveClick = async () => {
    await postReservation({
      fromDate: startDate,
      toDate: endDate,
      isQuickPickUp: quickPickup === "true",
      bookId: book.id,
    });
    console.log("Success");
  };

  return (
    <div>
      <img
        className="max-h-52 max-w-52"
        src={`data:image/jpeg;base64,${book.pictureData}`}
        alt={book.name}
      />
      <p>{book.year}</p>
      <h3>{book.name}</h3>
      <div>
        <input
          type="checkbox"
          value={quickPickup}
          onChange={(e) => setQuickPickup(e.target.value)}
        />
      </div>
      <DatePicker
        swapRange
        selected={startDate}
        onChange={onChange}
        startDate={startDate}
        endDate={endDate}
        minDate={new Date()}
        selectsRange
        selectsDisabledDaysInRange
      />
      <p>
        Total days: {reservationPrice.totalDays}
        <br />
        Service fee: {reservationPrice.serviceFee}
        <br />
        Quick pickup: {reservationPrice.quickPickupFee}
        <br />
        Discount: {reservationPrice.discountSum}
        <br />
        Total sum: {reservationPrice.totalSum}
        <br />
      </p>
      <button
        className="bg-gray-200"
        disabled={reservationPrice > 0}
        onClick={onReserveClick}
      >
        Reserve
      </button>
    </div>
  );
}
