import { getLibraryItems } from "./api/ApiLibrary";
import { useState, useEffect } from "react";
import LibraryItem from "./components/LibraryItem/LibraryItem";

export default function App() {
  const [libraryItems, setLibraryItems] = useState(null);
  const [search, setSearch] = useState('');

  useEffect(() => {
    getLibraryItems(search).then((res) => {
      setLibraryItems(res);
    });
  }, [search]);

  if (!libraryItems) {
    return (
      <div className="text-center max-w-screen-lg m-auto my-5 px-2">
        <h2 className="font-semibold text-lg">Loading...</h2>
      </div>
    );
  }

  if (libraryItems.length <= 0) {
    return (
      <div className="max-w-screen-lg m-auto my-5 px-2">
        <h2 className="font-semibold text-lg">
          No books could be found. Try again later.
        </h2>
      </div>
    );
  } else {
    return (
      <div className="max-w-screen-lg m-auto my-5 px-2">
        <input className="border-black border-2" value={search} onChange={e => setSearch(e.target.value)} />
        <div className="flex flex-wrap">
          {libraryItems.map((item, index) => (
            <LibraryItem book={item} key={index} />
          ))}
        </div>
      </div>
    );
  }
}
