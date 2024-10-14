export default function BookReservation({ book }) {
  return (
    <div className="w-full">
      <img
        className="max-h-80 max-w-80 object-cover m-auto"
        src={`data:image/jpeg;base64,${book.pictureData}`}
        alt={book.name}
      />
      <div>
        <div className="text-sm font-light flex gap-3">
          <span>Release date: {book.year.split("T")[0]}</span>
          <span>Book type: {book.bookType}</span>
        </div>
        <h2 className="font-bold text-2xl py-2 leading-none">{book.name}</h2>
      </div>
    </div>
  );
}
