import axios from "axios";

const url = `${import.meta.env.VITE_URL}:${
  import.meta.env.VITE_PORT
}/api`;

const getReservations = async () => {
  return (await axios.get(`${url}/reservations`)).data;
};

const getReservation = async (id) => {
  return (await axios.get(`${url}/${id}`)).data;
};

const postReservation = async (reservation) => {    
  await axios.post(`${url}/reservations`, reservation);
};

const getReservationPrice = async (fromDate, toDate, isQuickPickup, bookId) => {
  return await axios.get(`${url}/reservationPrice`, {
    params: {
      createdAt: Date.now(),
      fromDate: fromDate,
      toDate: toDate,
      isQuickPickUp: isQuickPickup,
      bookId: bookId,
    },
  });
};

export {
  getReservations,
  getReservation,
  postReservation,
  getReservationPrice,
};
