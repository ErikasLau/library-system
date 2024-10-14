import { useEffect, useState } from "react";
import { getReservations } from "../../api/ApiReservations";
import Reservation from "../Reservation/Reservation";

export default function ReservationsPage() {
  const [reservations, setReservations] = useState(null);

  useEffect(() => {
    getReservations().then((res) => {
      setReservations(res);
      console.log(res);
    });
  }, []);

  if (!reservations) {
    return (
      <div className="text-center max-w-screen-lg m-auto my-5 px-2">
        <h2 className="font-semibold text-lg">Loading...</h2>
      </div>
    );
  }

  if (reservations.length <= 0) {
    return (
      <div className="max-w-screen-lg m-auto my-5 px-2">
        <h2 className="font-semibold text-lg">
          No reservations could be found.
        </h2>
      </div>
    );
  } else {
    return (
      <div className="max-w-screen-lg m-auto my-5 px-2">
        <div className="flex flex-col gap-3">
          {reservations.map((element, index) => (
            <Reservation reservation={element} key={index} />
          ))}
        </div>
      </div>
    );
  }
}
