import { useEffect, useRef, useState } from "react";
import { Link } from "react-router-dom";
import { useAppSelector } from "../hooks/reduxHooks";

// Add this style to your css file

const Navbar = () => {
  const { user } = useAppSelector((state) => state.user);
  const [state, setState] = useState(false);
  const navRef = useRef<HTMLElement>(null);

  // Replace / path with your path
  // const navigation = [
  //   { title: "Books", path: "/" },
  //   { title: "Loans", path: "/loans" },
  // ];

  useEffect(() => {
    const body = document.body;

    // Disable scrolling
    const customBodyStyle = ["overflow-hidden", "lg:overflow-visible"];
    if (state) body.classList.add(...customBodyStyle);
    // Enable scrolling
    else body.classList.remove(...customBodyStyle);

    // Sticky strick
    const customStyle = ["sticky-nav", "fixed", "border-b"];
    window.onscroll = () => {
      if (navRef.current) {
        if (window.scrollY > 80) navRef?.current.classList.add(...customStyle);
        else navRef.current.classList.remove(...customStyle);
      }
    };
  }, [state]);

  return (
    <nav ref={navRef} className="bg-white w-full top-0 z-20">
      <div className="items-center px-4 max-w-screen-3xl mx-auto md:px-8 lg:flex">
        <div className="flex items-center justify-between py-3 lg:py-4 lg:block">
          <Link to="/" className="text-3xl">
            BooKing
          </Link>
          <div className="lg:hidden">
            <button
              className="text-gray-700 outline-none p-2 rounded-md focus:border-gray-400 focus:border"
              onClick={() => setState(!state)}
            >
              {state ? (
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  className="h-6 w-6"
                  viewBox="0 0 20 20"
                  fill="currentColor"
                >
                  <path
                    fillRule="evenodd"
                    d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                    clipRule="evenodd"
                  />
                </svg>
              ) : (
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  className="h-6 w-6"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M4 8h16M4 16h16"
                  />
                </svg>
              )}
            </button>
          </div>
        </div>
        <div
          className={`flex-1 justify-between flex-row-reverse lg:overflow-visible lg:flex lg:pb-0 lg:pr-0 lg:h-auto ${
            state ? "h-screen pb-20 overflow-auto pr-4" : "hidden"
          }`}
        >
          <div>
            <ul className="flex flex-col-reverse space-x-0 items-center lg:space-x-6 lg:flex-row">
              {user ? (
                <>
                  <li className="mt-8 mb-8 lg:mt-0 lg:mb-0">
                    <Link
                      to="/loans"
                      className="text-gray-600 hover:text-indigo-600"
                    >
                      Loans
                    </Link>
                  </li>
                  <li className="mt-8 mb-8 lg:mt-0 lg:mb-0">
                    <Link
                      to="/profile"
                      className="text-gray-600 hover:text-indigo-600"
                    >
                      Profile
                    </Link>
                  </li>

                  {user.role === "Admin" && (
                    <li className="mt-8 mb-8 lg:mt-0 lg:mb-0">
                      <Link
                        to="/admin"
                        className="text-gray-600 hover:text-indigo-600"
                      >
                        Admin
                      </Link>
                    </li>
                  )}

                  <li className="mt-8 lg:mt-0">
                    <button className="py-3 px-4 text-center text-white bg-indigo-600 hover:bg-indigo-700 rounded-md shadow block lg:inline">
                      Logout
                    </button>
                  </li>
                </>
              ) : (
                <>
                  <li className="mt-4 lg:mt-0">
                    <Link
                      to="/login"
                      className="py-3 px-4 text-center border text-gray-600 hover:text-indigo-600 rounded-md block lg:inline lg:border-0"
                    >
                      Login
                    </Link>
                  </li>
                  <li className="mt-8 lg:mt-0">
                    <Link
                      to="/create-account"
                      className="py-3 px-4 text-center text-white bg-indigo-600 hover:bg-indigo-700 rounded-md shadow block lg:inline"
                    >
                      Sign Up
                    </Link>
                  </li>
                </>
              )}
            </ul>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
