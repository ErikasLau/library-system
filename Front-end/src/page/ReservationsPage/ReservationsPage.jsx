import { useEffect, useState } from "react";
import { getReservations } from "../../api/ApiReservations";
import Reservation from "../../components/Reservation/Reservation";
import Button from "../../components/Button/Button";

export default function ReservationsPage() {
  const [reservations, setReservations] = useState(null);
  const [error, setError] = useState(false);

  useEffect(() => {
    try {
      getReservations().then((res) => {
        setReservations(res);
      });
    } catch (error) {
      console.error("Fetch reservations", error);
      setError(true);
    }
  }, []);

  return (
    <div className="max-w-screen-lg m-auto my-5 px-2">
      {!reservations && (
        <div className="text-center">
          <h3>Loading...</h3>
        </div>
      )}
      {!reservations && error && (
        <div className="text-center lg:text-xl text-lg">
          <h2>
            There was an error while loading reservations. Please try again
            later.
          </h2>
        </div>
      )}
      {reservations && reservations.length <= 0 && (
        <div className="text-center">
          <h2 className="font-medium lg:text-xl text-lg">
            Sorry, no reservations could be found. Try creating a new one!
          </h2>
          <Button href="/library" buttonText="Go to library catalog" />
        </div>
      )}
      {reservations && reservations.length > 0 && (
        <div className="flex flex-col gap-3">
          {reservations.map((element, index) => (
            <Reservation reservation={element} key={index} />
          ))}
        </div>
      )}
    </div>
  );
}
