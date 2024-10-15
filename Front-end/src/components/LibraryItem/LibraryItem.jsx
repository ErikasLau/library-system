export default function LibraryItem({ book }) {
  const formattedYear = new Date(book.year).getFullYear();

  return (
    <div className="lg:basis-1/4 sm:basis-1/3 xsm:basis-1/2 basis-full grid grid-cols-1">
      <div className="border-black border-2 m-2 rounded-md p-2 bg-gray-50 max-w-full flex flex-col justify-between">
        <div>
          <img
            className="object-cover aspect-square w-full max-h-full"
            src={`data:image/jpeg;base64,${book.pictureData}`}
            alt={book.name}
          />
          <div>
            <div className="text-sm">
              <span className="text-gray-500">{formattedYear}</span> ·{" "}
              {book.bookType}
            </div>
            <h2 className="font-bold text-2xl py-2 leading-none">
              {book.name}
            </h2>
          </div>
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
