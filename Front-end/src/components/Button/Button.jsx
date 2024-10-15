export default function Button({ href, buttonText }) {
  return (
    <a
      href={href}
      className="inline-flex text-white bg-black hover:bg-gray-800 font-medium rounded-lg text-sm px-5 py-2.5 text-center my-4"
    >
      {buttonText}
    </a>
  );
}
