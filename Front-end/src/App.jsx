import { getLibraryItems } from "./api/ApiLibrary";
import { useState, useEffect } from "react";
import LibraryItem from "./components/LibraryItem/LibraryItem";

export default function App() {  
  const [libraryItems, setLibraryItems] = useState([]);
  const [search, setSearch] = useState("");
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await getLibraryItems(search);
        setLibraryItems(response);
      } catch (error) {
        console.error("Error fetching library items:", error);
        setError(true);
      } finally {
        setIsLoading(false);
      }
    };

    const timeoutId = setTimeout(fetchData, 500);

    return () => clearTimeout(timeoutId);
  }, [search]);

  return (
    <div className="max-w-screen-lg m-auto my-5 px-2">
      <input
        className="border-black border-2 w-full rounded-md p-2 outline-none hover:bg-gray-100 focus:bg-gray-100"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        placeholder="Start typing to search for books..."
      />
      {isLoading && <h2 className="text-center">Loading...</h2>}
      {error && (
        <h2 className="text-center">
          There was an error while loading all of the books catalog.
        </h2>
      )}
      {libraryItems.length === 0 && !isLoading && !error && (
        <h2 className="text-center">
          No books could be found. Try a different search term.
        </h2>
      )}
      {libraryItems.length > 0 && !error && (
        <div className="flex flex-wrap">
          {libraryItems.map((item, index) => (
            <LibraryItem book={item} key={index} />
          ))}
        </div>
      )}
    </div>
  );
}
