export default function LibraryItem({book}) {
  return (
    <div className="border-black border-solid border-2 border-r-4">
      <img className="max-h-52 max-w-52" src={`data:image/jpeg;base64,${book.pictureData}`} alt={book.name} />
      <p>{book.year}</p>
      <h3>{book.name}</h3>
      <a href={`/library/${book.id}`}>
        <button className="bg-gray-200">Reserve</button>
      </a>
    </div>
  );
}