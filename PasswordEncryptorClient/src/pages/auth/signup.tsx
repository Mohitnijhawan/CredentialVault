import { useForm } from "react-hook-form";
import type { SignUpRequest } from "../../models/signup/SignUpRequest";
import { zodResolver } from "@hookform/resolvers/zod";
import { SignUpSchema } from "../../models/schema/SignUpSchema";
import { SignUp } from "../../core/services/authService";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import pic from "../../assets/pic.png";

const Signup = () => {
  const { register, handleSubmit, formState: { errors } } =
    useForm<SignUpRequest>({
      resolver: zodResolver(SignUpSchema),
    });

  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);

  const submit = async (model: SignUpRequest) => {
    const data = await SignUp(model);
    if (data.isSuccess) {
      toast.success(data.message);
    }
  };

  return (
    <div className="relative h-screen flex items-center justify-center px-4 sm:px-6 lg:px-8 overflow-hidden">
      {/* 🔥 FULL SCREEN BACKGROUND IMAGE */}
      <div className="absolute inset-0 -z-10">
        <img
          src={pic}
          alt="bg"
          className="absolute inset-0 w-full h-full object-cover object-center"
        />
        {/* lighter overlay so image clear rahe */}
        <div className="absolute inset-0 bg-black/40" />
      </div>

      {/* Content Wrapper */}
      <div className="relative z-10 w-full max-w-md sm:max-w-lg">
        {/* Heading */}
        <div className="text-center text-white mb-6 sm:mb-8">
          <h1 className="text-3xl sm:text-4xl font-bold">SecureVault 🔐</h1>
          <p className="text-gray-300 mt-2 text-sm sm:text-base">
            Store and manage your passwords securely
          </p>
        </div>

        {/* Form */}
        <form
          onSubmit={handleSubmit(submit)}
          className="bg-white/10 backdrop-blur-md p-5 sm:p-6 rounded-2xl shadow-lg space-y-4 text-white"
        >
          <h2 className="text-xl sm:text-2xl font-semibold text-center">
            Create Account
          </h2>

          {/* Email */}
          <div>
            <label className="block text-sm mb-1">Email</label>
            <input
              type="email"
              {...register("email")}
              placeholder="Enter your email"
              className="w-full px-3 py-2 rounded-lg bg-white/20 placeholder-gray-300 
              focus:outline-none focus:ring-2 focus:ring-blue-500 text-sm sm:text-base"
            />
            {errors?.email && (
              <p className="text-red-400 text-xs sm:text-sm mt-1">
                {errors.email.message}
              </p>
            )}
          </div>

          {/* 🔥 PASSWORD WITH SHOW */}
          <div>
            <label className="block text-sm mb-1">Password</label>

            <div className="relative">
              <input
                type={showPassword ? "text" : "password"}
                {...register("password")}
                placeholder="Enter your password"
                className="w-full px-3 py-2 pr-14 rounded-lg bg-white/20 
                placeholder-gray-300 focus:outline-none focus:ring-2 
                focus:ring-blue-500 text-sm sm:text-base"
              />

              <button
                type="button"
                onClick={() => setShowPassword(!showPassword)}
                className="absolute right-3 top-1/2 -translate-y-1/2 text-white font-semibold text-sm hover:text-blue-400"
              >
                {showPassword ? "HIDE" : "SHOW"}
              </button>
            </div>

            {errors?.password && (
              <p className="text-red-400 text-xs sm:text-sm mt-1">
                {errors.password.message}
              </p>
            )}
          </div>

          {/* Contact */}
          <div>
            <label className="block text-sm mb-1">Contact No</label>
            <input
              type="text"
              {...register("contactNo")}
              placeholder="Enter your number"
              className="w-full px-3 py-2 rounded-lg bg-white/20 placeholder-gray-300 
              focus:outline-none focus:ring-2 focus:ring-blue-500 text-sm sm:text-base"
            />
            {errors?.contactNo && (
              <p className="text-red-400 text-xs sm:text-sm mt-1">
                {errors.contactNo.message}
              </p>
            )}
          </div>

          {/* Button */}
          <button
            type="submit"
            className="w-full bg-blue-600 py-2 rounded-lg hover:bg-blue-700 transition text-sm sm:text-base"
          >
            Sign Up
          </button>

          {/* Redirect */}
          <p className="text-xs sm:text-sm text-center mt-2">
            Already have an account?{" "}
            <span
              className="text-blue-400 cursor-pointer hover:underline"
              onClick={() => navigate("/login")}
            >
              Login
            </span>
          </p>
        </form>

        {/* Footer */}
        <p className="text-center text-gray-400 text-xs sm:text-sm mt-6">
          © 2026 SecureVault | Built by Mohit
        </p>
      </div>
    </div>
  );
};

export default Signup;