import { getLibraryItems } from "./api/ApiLibrary";
import Header from "./components/Header";
import { useState, useEffect } from "react";
import LibraryItem from "./components/LibraryItem/LibraryItem";

export default function App() {
  const [libraryItems, setLibraryItems] = useState([]);

  useEffect(() => {
    getLibraryItems().then((res) => {
      setLibraryItems(res);
    });
  }, []);

  return (
    <div>
      <Header />
      {libraryItems.map((item, index) => (
        <div key={index}>
          <LibraryItem book={item} />
        </div>
      ))}
    </div>
  );
}
