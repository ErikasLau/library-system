import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import BookReservationPage from "../page/BookReservationPage/BookReservationPage";
import { getLibraryItem } from "../api/ApiLibrary";
import ReservationsPage from "../page/ReservationsPage/ReservationsPage";
import NotFoundPage from "../page/NotFoundPage/NotFoundPage";
import LibraryCatalogPage from "../page/LibraryCatalogPage/LibraryCatalogPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "library/:bookId",
    element: <BookReservationPage />,
    loader: async ({ params }) => {
      return await getLibraryItem(params.bookId);
    },
  },
  {
    path: "reservations",
    element: <ReservationsPage />,
  },
  {
    path: "library",
    element: <LibraryCatalogPage />,
  },
]);

export default router;
