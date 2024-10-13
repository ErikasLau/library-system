export default function Reservation({reservation}){
    console.log(reservation);
    
    return(
       <div>
        <div>
            {reservation.createdAt}
        </div>
        <div>
            {reservation.book.name}
        </div>
        <div>
            {reservation.book.bookType}
        </div>
        <div>
            {reservation.isQuickPickUp ? "true" : "false"}
        </div>
        <div>
            {reservation.fromDate}
        </div>
        <div>
            {reservation.toDate}
        </div>
       </div> 
    )
}