import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { getBooks } from "./redux/reducers/bookReducers";
import "react-toastify/dist/ReactToastify.css";
import { useEffect } from "react";

import Root from "./layout/Root";
import Login from "./pages/Login";
import Home from "./pages/Home";
import SignUp from "./pages/SignUp";
import { useAppDispatch } from "./hooks/reduxHooks";
import Book from "./pages/Book";
import { getUser } from "./redux/reducers/userReducers";
import Profile from "./pages/Profile";
import Dashboard from "./layout/Dashboard";
import Overview from "./pages/Overview";
import Books from "./pages/Books";
import CreateBooks from "./pages/CreateBooks";
// import Users from "./pages/Users";
import CreateAdmin from "./pages/CreateAdmin";
// import UpdateUser from "./pages/UpdateUser";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    children: [
      {
        path: "",
        element: <Home />,
      },
      {
        path: "book/:id",
        element: <Book />,
      },

      {
        path: "profile",
        element: <Profile />,
      },
    ],
  },
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: "/create-account",
    element: <SignUp />,
  },
  {
    path: "/admin",
    element: <Dashboard />,
    children: [
      {
        path: "",
        element: <Overview />,
      },
      {
        path: "books",
        element: <Books />,
      },
      {
        path: "create-book",
        element: <CreateBooks />,
      },

      // {
      //   path: "users",
      //   element: <Users />,
      // },
      // {
      //   path: "users/:id",
      //   element: <UpdateUser />,
      // },

      {
        path: "create-admin",
        element: <CreateAdmin />,
      },
    ],
  },
]);

const App = () => {
  // axios.defaults.withCredentials =  true;
  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(getBooks());
    dispatch(getUser());
  }, []);
  return (
    <>
      <RouterProvider router={router} />
      <ToastContainer
        position="top-right"
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
      {/* Same as */}
      <ToastContainer />
    </>
  );
};

export default App;
