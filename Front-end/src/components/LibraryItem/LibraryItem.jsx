export default function LibraryItem({ book }) {
  const formattedYear = new Date(book.year).getFullYear();

  return (
    <div className="lg:basis-1/4 sm:basis-1/3 xsm:basis-1/2 basis-full">
      <div className="border-black border-2 m-2 rounded-md p-2 bg-gray-50 max-w-full">
        <img
          className="max-h-52 max-w-52 object-cover"
          src={`data:image/jpeg;base64,${book.pictureData}`}
          alt={book.name}
        />
        <div>
          <div className="text-sm font-light flex gap-2">
            <span>{formattedYear}</span>
            <span>{book.bookType}</span>
          </div>
          <h2 className="font-bold text-2xl py-2 leading-none">{book.name}</h2>
        </div>
        <a href={`/library/${book.id}`}>
          <button className="bg-gray-200 w-full rounded-md p-1 hover:bg-gray-300 duration-100">
            Reserve
          </button>
        </a>
      </div>
    </div>
  );
}
