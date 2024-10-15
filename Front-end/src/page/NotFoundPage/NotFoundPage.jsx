import Button from "../../components/Button/Button";

export default function NotFoundPage() {
  return (
    <div className="max-w-screen-lg m-auto my-5 px-2">
      <div className="mx-auto max-w-screen-sm text-center">
        <h1 className="mb-4 text-7xl tracking-tight font-extrabold lg:text-9xl text-gray-900">
          404
        </h1>
        <p className="mb-4 text-3xl tracking-tight font-bold text-gray-900 md:text-4xl">
          Somethings missing.
        </p>
        <p className="mb-4 text-lg font-light text-gray-600">
          Sorry, we could not find the page you were looking for.
        </p>
        <Button href="/" buttonText="Back to Homepage" />
      </div>
    </div>
  );
}
