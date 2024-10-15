export default function Header() {
  return (
    <header className="bg-gray-200">
      <nav className="flex flex-wrap justify-between items-center mx-auto max-w-7xl p-6 lg:px-8 gap-2">
        <div className="w-full">
          <a href="/" className="p-1.5 pl-0 font-semibold text-3xl">
            Library system.
          </a>
        </div>
        <div className="flex flex-row gap-3">
          <a href="/library" className="hover:underline">
            Library catalog
          </a>
          <a href="/reservations" className="hover:underline">
            Reservations
          </a>
        </div>
      </nav>
    </header>
  );
}
