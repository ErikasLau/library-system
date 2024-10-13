import { createBrowserRouter } from "react-router-dom";
import App from "../src/App";
import BookReservationPage from "../src/components/BookReservationPage/BookReservationPage";
import { getLibraryItem } from "../src/api/ApiLibrary";
import ReservationsPage from "../src/components/ReservationsPage/ReservationsPage";

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
    },
    {
        path: "library/:bookId",
        element: <BookReservationPage />,
        loader: async ({params}) => {
            return await getLibraryItem(params.bookId)
        },
    },
    {
        path: "reservations",
        element: <ReservationsPage />,
    },
]);

export default router;