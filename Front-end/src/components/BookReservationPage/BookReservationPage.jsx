import { useLoaderData } from "react-router-dom";
import DatePicker from "react-datepicker";
import { useEffect, useState } from "react";
import "react-datepicker/dist/react-datepicker.css";
import {
  getReservationPrice,
  postReservation,
} from "../../api/ApiReservations";
import BookReservation from "../BookReservation/BookReservation";
import ReservationCheck from "./ReservationCheck";

const defaultReservationDetails = {
  totalDays: 0,
  serviceFee: 0,
  quickPickupFee: 0,
  discountSum: 0,
  totalSum: 0,
};

export default function BookReservationPage() {
  const book = useLoaderData();

  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(null);
  const [reservationDetails, setReservationDetails] = useState(
    defaultReservationDetails
  );
  const [isQuickPickup, setIsQuickPickup] = useState(false);
  const [notification, setNotification] = useState({
    message: "",
    type: "error",
    show: true,
  });

  useEffect(() => {
    if (!notification.show) {
      const timeoutId = setTimeout(
        () =>
          setNotification({
            message: "",
            type: "error",
            show: true,
          }),
        5000
      );
      return () => clearTimeout(timeoutId);
    }
  }, [notification]);

  const onChange = (dates) => {
    const [start, end] = dates;
    setStartDate(start);
    setEndDate(end);

    if (end - start > 0) {
      getReservationPrice(start, end, isQuickPickup, book.id).then((res) =>
        setReservationDetails(res.data)
      );
    } else {
      setReservationDetails(defaultReservationDetails);
    }
  };

  const onCheckboxChange = () => {
    const value = !isQuickPickup;
    setIsQuickPickup(value);

    if (endDate - startDate > 0) {
      getReservationPrice(startDate, endDate, value, book.id).then((res) =>
        setReservationDetails(res.data)
      );
    } else {
      setReservationDetails(defaultReservationDetails);
    }
  };

  const onReserveClick = async () => {
    let notification = {
      message: "Reservation completed successfully!",
      type: "succes",
      show: false,
    };

    try {
      await postReservation({
        fromDate: startDate,
        toDate: endDate,
        isQuickPickUp: isQuickPickup,
        bookId: book.id,
      });
    } catch (error) {
      console.error("Reservation error:", error);
      notification = {
        message: "There was an error while completing reservation.",
        type: "error",
        show: false,
      };
    } finally {
      setStartDate(new Date());
      setEndDate(new Date());
      setReservationDetails(defaultReservationDetails);
      setIsQuickPickup(false);
      setNotification(notification);
    }
  };

  return (
    <div className="max-w-screen-lg m-auto my-5 px-2">
      <div
        className={
          notification.type === "error"
            ? "bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative"
            : "bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded relative"
        }
        hidden={notification.show}
      >
        <span className="block sm:inline">{notification.message}</span>
      </div>

      <div className="flex">
        <div className="md:basis-1/2 basis-full">
          <BookReservation book={book} />
        </div>
        <div className="md:basis-1/2 basis-full flex flex-col gap-2">
          <div className="text-center">
            <span className="flex gap-2 max-w-full">
              Quick pickup needed (5€):{" "}
              <input
                type="checkbox"
                checked={isQuickPickup}
                onChange={onCheckboxChange}
              />
            </span>
            <DatePicker
              selected={startDate}
              onChange={onChange}
              startDate={startDate}
              endDate={endDate}
              minDate={new Date()}
              selectsRange
              inline
              selectsDisabledDaysInRange
            />
          </div>
          <ReservationCheck reservationDetails={reservationDetails} />
          <button
            className="bg-gray-300 w-full p-2 rounded-md hover:bg-gray-400 duration-100 disabled:bg-slate-100"
            disabled={reservationDetails.totalDays === 0}
            onClick={onReserveClick}
          >
            Confirm reservation
          </button>
        </div>
      </div>
    </div>
  );
}
