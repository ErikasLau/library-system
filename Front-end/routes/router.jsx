import { createBrowserRouter } from "react-router-dom";
import App from "../src/App";
import BookReservationPage from "../src/components/BookReservationPage/BookReservationPage";
import { getLibraryItem } from "../src/api/ApiLibrary";
import ReservationsPage from "../src/components/ReservationsPage/ReservationsPage";
import NotFoundPage from "../src/components/NotFoundPage/NotFoundPage";

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        errorElement: <NotFoundPage />
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
    {
        path: "library",
        element: <App />,
    },
]);

export default router;