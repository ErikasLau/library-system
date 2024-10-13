import { useEffect, useState } from "react"
import { getReservations } from "../../api/ApiReservations";
import Reservation from "../Reservation/Reservation";

export default function ReservationsPage(){
    const [reservations, setReservations] = useState([]);

    useEffect(() => {
        getReservations().then(res => {
            setReservations(res)
            console.log(res);
            
        })
    }, [])

    return(
        <div>
            {reservations.map((element, index) => (
                <div key={index}>
                    <Reservation reservation={element} />
                </div>
            ))}
        </div>
    )
}