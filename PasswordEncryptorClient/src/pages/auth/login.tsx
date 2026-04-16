import { useForm } from "react-hook-form";
import type { LoginRequest } from "../../models/login/LoginRequest";
import { zodResolver } from "@hookform/resolvers/zod";
import { LoginSchema } from "../../models/schema/LoginSchema";
import { Login } from "../../core/services/authService";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import pic from "../../assets/pic.png";
import { ACCESS_TOKEN_KEY, REFRESH_TOKEN_KEY } from "../../constants/storage-names";

const LoginPage = () => {
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);

  const { register, handleSubmit, formState: { errors } } =
    useForm<LoginRequest>({
      resolver: zodResolver(LoginSchema)
    });

  const submit = async (model: LoginRequest) => {
    try {
      const data = await Login(model);

      if (data?.isSuccess && data?.data) {
        toast.success(data.message);

        localStorage.setItem(ACCESS_TOKEN_KEY, data.data.accessToken);
        localStorage.setItem(REFRESH_TOKEN_KEY, data.data.refreshToken);

        navigate("/dashboard");
      } else {
        toast.error(data?.message || "Login failed");
      }
    } catch {
      toast.error("Something went wrong");
    }
  };

  return (
    <div className="relative min-h-screen flex items-center justify-center px-4 sm:px-6 lg:px-8 overflow-hidden">

      {/* 🔥 PERFECT BACKGROUND (NO BUG EVER) */}
      <div
        className="absolute inset-0 -z-10 bg-cover bg-center bg-no-repeat"
        style={{ backgroundImage: `url(${pic})` }}
      >
        <div className="absolute inset-0 bg-black/40" />
      </div>

      {/* Content */}
      <div className="relative z-10 w-full max-w-md sm:max-w-lg">

        {/* Heading */}
        <div className="text-center text-white mb-6 sm:mb-8">
          <h1 className="text-3xl sm:text-4xl font-bold">
            SecureVault 🔐
          </h1>
          <p className="text-gray-300 mt-2 text-sm sm:text-base">
            Welcome back, access your vault securely
          </p>
        </div>

        {/* Form */}
        <form
          onSubmit={handleSubmit(submit)}
          className="bg-white/10 backdrop-blur-md p-5 sm:p-6 rounded-2xl shadow-lg space-y-5 text-white"
        >
          <h2 className="text-xl sm:text-2xl font-semibold text-center">
            Login
          </h2>

          {/* Email */}
          <div>
            <label className="block text-sm mb-1">Email</label>
            <input
              type="email"
              {...register("email")}
              placeholder="Enter your email"
              className="w-full px-3 py-2 rounded-lg bg-white/20 
              focus:outline-none focus:ring-2 focus:ring-blue-500"
            />
            <span className="text-red-400 text-sm mt-1 block">
              {errors?.email?.message}
            </span>
          </div>

          {/* 🔥 PASSWORD */}
          <div>
            <label className="block text-sm mb-1">Password</label>

            <div className="relative">
              <input
                type={showPassword ? "text" : "password"}
                {...register("password")}
                placeholder="Enter your password"
                className="w-full px-3 py-2 pr-14 rounded-lg bg-white/20 
                focus:outline-none focus:ring-2 focus:ring-blue-500"
              />

              <button
                type="button"
                onClick={() => setShowPassword(!showPassword)}
                className="absolute right-3 top-2 text-white font-semibold text-sm hover:text-blue-400"
              >
                {showPassword ? "HIDE" : "SHOW"}
              </button>
            </div>

            <span className="text-red-400 text-sm mt-1 block">
              {errors?.password?.message}
            </span>
          </div>

          {/* Button */}
          <button
            type="submit"
            className="w-full bg-blue-600 py-2.5 rounded-lg 
            hover:bg-blue-700 transition"
          >
            Login
          </button>
        </form>

        {/* Signup */}
        <p className="mt-5 text-center text-sm text-gray-300">
          Don't have an account?{" "}
          <span
            onClick={() => navigate("/signup")}
            className="text-blue-400 font-semibold cursor-pointer hover:underline"
          >
            Sign Up
          </span>
        </p>

        {/* Footer */}
        <p className="text-center text-gray-400 text-sm mt-6">
          © 2026 SecureVault | Built by Mohit
        </p>
      </div>
    </div>
  );
};

export default LoginPage;