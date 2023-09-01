import Navbar from "../components/Navbar";

import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { useState } from "react";
import {AiFillEyeInvisible, AiFillEye} from 'react-icons/ai'
import axios from "axios";
import { toast } from "react-toastify";
import { Link, useNavigate } from "react-router-dom";
const schema = yup.object({
  firstName: yup.string().required("First name is required"),
  lastName: yup.string().required("Last name is required"),
  email: yup
    .string()
    .required("Email is required")
    .email("Enter a valid email address"),
  gender: yup.string().required("Gender is required"),
  avatar: yup
    .string()
    .required("Avatar is required")
    .url("avatar must be a valid image url"),
  role: yup.string().required("Role is required"),
  password: yup
    .string()
    .required("Password is required")
    .min(6, "Password length should be at least 6 characters")
    .max(12, "Password cannot exceed more than 12 characters"),

  confirm_password: yup
    .string()
    .required("Confirm Password is required")
    .min(4, "Password length should be at least 4 characters")
    .max(12, "Password cannot exceed more than 12 characters")
    .oneOf([yup.ref("password")], "Passwords do not match"),
});

const SignUp = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
      resolver: yupResolver(schema),
      defaultValues: {
          role:"user"
      }
  });
    const [showPassword, setShowPassword] = useState(false);

    const navigate  =  useNavigate()
    const onSubmit = handleSubmit(async(data) => {
        try {
   
            
            const response = await axios.post(
                "https://libmgtsyst.azurewebsites.net/api/v1/users",
                data
                
            );

            if (response.status === 201) {
                toast.success('user created successfully')
                setTimeout(() => navigate('/login'), 100)
                
         }
        } catch (error) {
            
        }
    })

  return (
    <>
      <Navbar />
      <div className="bg-white py-6 sm:py-8 lg:py-12">
        <div className="mx-auto max-w-screen-2xl px-4 md:px-8">
          <h2 className="mb-4 text-center text-2xl font-bold text-gray-800 md:mb-8 lg:text-3xl">
            Login
          </h2>

          <form
            className="mx-auto max-w-lg rounded-lg border"
            onSubmit={onSubmit}
          >
            <div className="flex flex-col gap-4 p-4 md:p-8">
              <div>
                <label
                  htmlFor="firstName"
                  className="mb-2 inline-block text-sm text-gray-800 sm:text-base"
                >
                  First Name
                </label>
                <input
                  type="text"
                  className="w-full rounded border bg-gray-50 px-3 py-2 text-gray-800 outline-none ring-indigo-300 transition duration-100 focus:ring"
                  {...register("firstName")}
                />
                {errors.firstName && (
                  <p className="text-red-500 text-sm mt-1">
                    {errors.firstName.message}
                  </p>
                )}
              </div>
              <div>
                <label
                  htmlFor="lastName"
                  className="mb-2 inline-block text-sm text-gray-800 sm:text-base"
                >
                  Last Name
                </label>
                <input
                  className="w-full rounded border bg-gray-50 px-3 py-2 text-gray-800 outline-none ring-indigo-300 transition duration-100 focus:ring"
                  {...register("lastName")}
                />
                {errors.lastName && (
                  <p className="text-red-500 text-sm mt-1">
                    {errors.lastName.message}
                  </p>
                )}
              </div>

              <div>
                <label
                  htmlFor="firstName"
                  className="mb-2 inline-block text-sm text-gray-800 sm:text-base"
                >
                  Avatar
                </label>
                <input
                  type="text"
                  className="w-full rounded border bg-gray-50 px-3 py-2 text-gray-800 outline-none ring-indigo-300 transition duration-100 focus:ring"
                  {...register("avatar")}
                />
                {errors.avatar && (
                  <p className="text-red-500 text-sm mt-1">
                    {errors.avatar.message}
                  </p>
                )}
              </div>

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
                  htmlFor="gender"
                  className="mb-2 inline-block text-sm text-gray-800 sm:text-base"
                >
                  Gender
                </label>
                <select
                  {...register("gender")}
                  id=""
                  className="w-full rounded border bg-gray-50 px-3 py-2 text-gray-800 outline-none ring-indigo-300 transition duration-100 focus:ring"
                >
                  <option value="male">male</option>
                  <option value="female">female</option>
                </select>
                {errors.gender && (
                  <p className="text-red-500 text-sm mt-1">
                    {errors.gender.message}
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
              <div>
                <label
                  htmlFor="confirm_password"
                  className="mb-2 inline-block text-sm text-gray-800 sm:text-base"
                >
                  Confirm Password
                </label>
                <input
                  className="w-full rounded border bg-gray-50 px-3 py-2 text-gray-800 outline-none ring-indigo-300 transition duration-100 focus:ring"
                  {...register("confirm_password")}
                />
                {errors.confirm_password && (
                  <p className="text-red-500 text-sm mt-1">
                    {errors.confirm_password.message}
                  </p>
                )}
              </div>
              <button className="block rounded-lg bg-gray-800 px-8 py-3 text-center text-sm font-semibold text-white outline-none ring-gray-300 transition duration-100 hover:bg-gray-700 focus-visible:ring active:bg-gray-600 md:text-base">
                Create Account
              </button>

  

            
            </div>

            <div className="flex items-center justify-center bg-gray-100 p-4">
              <p className="text-center text-sm text-gray-500">
                Aready have an account?{" "}
                <Link
                  to="/login"
                  className="text-indigo-500 transition duration-100 hover:text-indigo-600 active:text-indigo-700"
                >
                  Login
                </Link>
              </p>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default SignUp;
