export default function Search({ search, setSearch, searchText }) {
  return (
    <input
      className="border-black border-2 w-full rounded-md p-2 outline-none hover:bg-gray-100 focus:bg-gray-100"
      value={search}
      onChange={(e) => setSearch(e.target.value)}
      placeholder={searchText}
    />
  );
}
