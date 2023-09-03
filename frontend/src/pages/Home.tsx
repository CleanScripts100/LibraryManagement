import { Link } from "react-router-dom";
// import { useState } from "react";

import { useAppSelector } from "../hooks/reduxHooks";

const Home = () => {
  // const [open, setOpen] = useState(false);
  // const [selectedColor, setSelectedColor] = useState(product.colors[0]);
  // const [selectedSize, setSelectedSize] = useState(product.sizes[2]);

  const { books } = useAppSelector((state) => state.books);
  return (
    <section className="py-32">
      <div className="max-w-screen-xl mx-auto px-4 md:px-8">
        <div className="space-y-5 sm:text-center sm:max-w-md sm:mx-auto">
          <h1 className="text-gray-800 text-3xl font-extrabold sm:text-4xl">
            Latest Books
          </h1>
          <p className="text-gray-600">Search for Books that interest you</p>
          <form
            onSubmit={(e) => e.preventDefault()}
            className="items-center justify-center gap-3 sm:flex"
          >
            <div className="relative">
              <div className="form">
                <button>
                  <svg
                    width="17"
                    height="16"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                    role="img"
                    aria-labelledby="search"
                  >
                    <path
                      d="M7.667 12.667A5.333 5.333 0 107.667 2a5.333 5.333 0 000 10.667zM14.334 14l-2.9-2.9"
                      stroke="currentColor"
                      strokeWidth="1.333"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                    ></path>
                  </svg>
                </button>
                <input
                  className="input"
                  placeholder="Type your text"
                  required
                  type="text"
                />
                <button className="reset" type="reset">
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-6 w-6"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                    strokeWidth="2"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="M6 18L18 6M6 6l12 12"
                    ></path>
                  </svg>
                </button>
              </div>
            </div>
          </form>
        </div>
        <ul className="grid gap-x-8 gap-y-10 mt-16 sm:grid-cols-2 lg:grid-cols-3">
          {books.map((items) => (
            <div
              className="rounded overflow-hidden shadow-lg p-2"
              key={items.id}
            >
              <img
                className="w-full h-40"
                src={items.images[0]}
                alt="Mountain"
              />
              <div className="px-6 py-4">
                <div className="font-bold text-xl mb-2">{items.title}</div>
                <p className="text-gray-700 text-base">
                  {items.description.substring(0, 20)}
                </p>
              </div>
              <div className="px-6 pt-4 pb-2">
                <span className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">
                  By: {items.author[0]}
                </span>
              </div>

              <Link
                to={`/book/${items.id}`}
                className="bg-indigo-800 hover:bg-indigo-600 py-2 text-center text-white block w-full shadow rounded text-lg"
              >
                View
              </Link>
            </div>
          ))}
        </ul>
      </div>
    </section>
  );
};

export default Home;
