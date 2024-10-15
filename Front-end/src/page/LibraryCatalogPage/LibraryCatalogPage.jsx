import { getLibraryItems } from "../../api/ApiLibrary";
import { useState, useEffect } from "react";
import LibraryItem from "../../components/LibraryItem/LibraryItem";
import Search from "../../components/Search/Search";

export default function LibraryCatalogPage() {
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
      <Search
        search={search}
        setSearch={setSearch}
        searchText="Start typing to search for books..."
      />
      <div className="m-4">
        {isLoading && <h2 className="text-center">Loading...</h2>}
        {error && (
          <h2 className="text-center font-medium lg:text-xl text-lg">
            There was an error while loading all of the books catalog.
          </h2>
        )}
        {libraryItems.length === 0 && !isLoading && !error && (
          <h2 className="text-center font-medium lg:text-xl text-lg">
            Sorry, no books could be found.
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
    </div>
  );
}
