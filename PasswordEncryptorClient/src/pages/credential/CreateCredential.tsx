import { useForm } from "react-hook-form"
import type { CredentialRequest } from "../../models/credential/CredentialRequest"
import { zodResolver } from "@hookform/resolvers/zod"
import { CredentialRequestSchema } from "../../models/schema/CredentialSchema"
import { createCredential } from "../../core/services/credentialService"
import { toast } from "react-toastify"
import { useNavigate } from "react-router-dom"
import { useState } from "react"

const CreateCredential = () => {
  const navigate=useNavigate();
  const {register,handleSubmit,formState:{errors}}=  useForm<CredentialRequest>({
        resolver:zodResolver(CredentialRequestSchema)
    })

      const [showPassword, setShowPassword] = useState(false);
    

    const submit=async(model:CredentialRequest)=>{
        const data=await createCredential(model);
        if(data.isSuccess){
            toast.success(data.message)
        }
    }
  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-gray-50 to-blue-50 px-4">
  
  <div className="w-full max-w-md">
    
    <form 
      onSubmit={handleSubmit(submit)} 
      className="bg-white p-8 rounded-2xl shadow-lg space-y-5"
    >
      <h1 className="text-2xl font-bold text-center text-gray-800">
        Add Credential 🔐
      </h1>

      <p className="text-center text-sm text-gray-500">
        Save your credentials securely
      </p>

      {/* Title */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Title
        </label>
        <input
          type="text"
          placeholder="e.g. Gmail, Facebook"
          {...register("title")}
          className="w-full border border-gray-300 rounded-xl px-3 py-2.5 
          focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition"
        />
        <span className="text-red-500 text-xs mt-1 block">
          {errors?.title?.message}
        </span>
      </div>

      {/* Username */}
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-1">
          Username
        </label>
        <input
          type="text"
          placeholder="Enter username"
          {...register("userName")}
          className="w-full border border-gray-300 rounded-xl px-3 py-2.5 
          focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition"
        />
        <span className="text-red-500 text-xs mt-1 block">
          {errors?.userName?.message}
        </span>
      </div>

      {/* Password */}
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
            <span>
              {errors.password && errors.password.message}
            </span>

      {/* Button */}
      <button onClick={()=>navigate("/dashboard")}
        type="submit"
        className="w-full bg-blue-600 text-white py-2.5 rounded-xl 
        font-medium hover:bg-blue-700 transition active:scale-[0.98]"
      >
        Save Credential
      </button>
    </form>

  </div>
</div>
  )
}

export default CreateCredential
