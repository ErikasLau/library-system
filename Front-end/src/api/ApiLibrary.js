import axios from "axios";

const url = "https://localhost:7261/api/library";

const getLibraryItems = async (search) => {
  return (await axios.get(url, { params: { search: search } })).data;
};

const getLibraryItem = async (id) => {
  return (await axios.get(`${url}/${id}`)).data;
};

const postBook = async (book) => {
  await axios.post(url, book);
};

const postAudiobook = async (audiobook) => {
  await axios.post(url, audiobook);
};

export { getLibraryItem, getLibraryItems, postBook, postAudiobook };
