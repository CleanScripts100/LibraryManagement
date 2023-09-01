
import Navbar from "../components/Navbar";
import * as yup from "yup";
import { useState } from "react";
import { AiFillEyeInvisible, AiFillEye } from "react-icons/ai";
import axios from "axios";
import { toast } from "react-toastify";
import { Link, useNavigate } from "react-router-dom";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { useAppDispatch } from "../hooks/reduxHooks";
import { getUser } from "../redux/reducers/userReducers";
const schema = yup.object({

  email: yup
    .string()
    .required("Email is required")
    .email("Enter a valid email address"),


  password: yup
    .string()
    .required("Password is required")


});
const Login = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(schema),
    defaultValues: {
     
    },
  });
  const [showPassword, setShowPassword] = useState(false);
    const navigate = useNavigate();
const dispatch = useAppDispatch()
     const onSubmit = handleSubmit(async (data) => {
       try {
         const response = await axios.post(
           "https://libmgtsyst.azurewebsites.net/api/v1/auth",
           data
         );

           console.log(response);
           localStorage.setItem('token', response.data);
         if (response.status === 200) {
           toast.success("Login successfully");
           dispatch(getUser())
           setTimeout(() => navigate("/"), 500);
         }
       } catch (error) {
toast.error('invalid login credentials')
    
       }
     });

    return (
      <>
        <Navbar />
        <div className="bg-white py-6 sm:py-8 lg:py-12">
          <div className="mx-auto max-w-screen-2xl px-4 md:px-8">
            <h2 className="mb-4 text-center text-2xl font-bold text-gray-800 md:mb-8 lg:text-3xl">
              Login
            </h2>

            <form className="mx-auto max-w-lg rounded-lg border" onSubmit={onSubmit}>
              <div className="flex flex-col gap-4 p-4 md:p-8">
                <div>
                  <label
                    htmlFor="email"
                    className="mb-2 inline-block text-sm text-gray-800 sm:text-base"
                  >
                    Email
                  </label>
                  <input
                    className="w-full rounded border bg-gray-50 px-3 py-2 text-gray-800 outline-none ring-indigo-300 transition duration-100 focus:ring"
                    {...register("email")}
                  />
                  {errors.email && (
                    <p className="text-red-500 text-sm mt-1">
                      {errors.email.message}
                    </p>
                  )}
                </div>

                <div>
                  <label
                    htmlFor="password"
                    className="mb-2 inline-block text-sm text-gray-800 sm:text-base"
                  >
                    Password
                  </label>
                  <div className="relative">
                    <input
                      type={showPassword ? "text" : "password"}
                      className="w-full rounded border bg-gray-50 px-3 py-2 text-gray-800 outline-none ring-indigo-300 transition duration-100 focus:ring"
                      {...register("password")}
                    />
                                    <button
                                        type="button"
                      className="absolute right-3 top-1/2 transform -translate-y-1/2 focus:outline-none"
                      onClick={() => setShowPassword(!showPassword)}
                    >
                      {showPassword ? <AiFillEyeInvisible /> : <AiFillEye />}
                    </button>
                  </div>
                  {errors.password && (
                    <p className="text-red-500 text-sm mt-1">
                      {errors.password.message}
                    </p>
                  )}
                </div>

                <button type="submit" className="block rounded-lg bg-gray-800 px-8 py-3 text-center text-sm font-semibold text-white outline-none ring-gray-300 transition duration-100 hover:bg-gray-700 focus-visible:ring active:bg-gray-600 md:text-base">
                  Log in
                </button>

       

               
              </div>

              <div className="flex items-center justify-center bg-gray-100 p-4">
                <p className="text-center text-sm text-gray-500">
                  Don't have an account?{" "}
                  <Link
                    to="/create-account"
                    className="text-indigo-500 transition duration-100 hover:text-indigo-600 active:text-indigo-700"
                  >
                    Register
                  </Link>
                </p>
              </div>
            </form>
          </div>
        </div>
      </>
    );
};

export default Login;
