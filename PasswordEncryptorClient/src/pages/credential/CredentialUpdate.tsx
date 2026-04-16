import { useNavigate, useParams } from "react-router-dom"
import { getCredentialById, updateCredential } from "../../core/services/credentialService";
import { useForm } from "react-hook-form";
import type { CredentialUpdateRequest } from "../../models/credential/CredentialRequest";
import { useEffect } from "react";

import { toast } from "react-toastify";

const CredentialUpdate = () => {
    const { register, reset, handleSubmit, formState: { errors } } = useForm<CredentialUpdateRequest>();
    
    const { id } = useParams();
    const navigate=useNavigate();

    const GetByid = async () => {
        const data = await getCredentialById(id as string);
        if (data.isSuccess) {
            reset({
                id:data.data.id,
                title: data.data.title,
                username: data.data.username
            })
        }
    }
    useEffect(() => {
        GetByid();
    }, [])

    const onsubmit = async (model: CredentialUpdateRequest) => {
        const response = await updateCredential(model);
        if (response.isSuccess) {
            toast.success(response.message)
        }
    }

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-100 px-4">
            <form
                onSubmit={handleSubmit(onsubmit)}
                className="w-full max-w-md bg-white p-6 rounded-2xl shadow-md space-y-5"
            >
                <h2 className="text-2xl font-semibold text-center text-gray-800">
                    Update Credential 🔄
                </h2>

                {/* Title */}
                <div>
                    <label className="block text-sm font-medium mb-1">Title</label>
                    <input
                        type="text"
                        {...register("title")}
                        className="w-full border rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                    />
                    <span className="text-red-500 text-sm">
                        {errors?.title?.message}
                    </span>
                </div>

                {/* Username */}
                <div>
                    <label className="block text-sm font-medium mb-1">Username</label>
                    <input
                        type="text"
                        {...register("username")}
                        className="w-full border rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                    />
                    <span className="text-red-500 text-sm">
                        {errors?.username?.message}
                    </span>
                </div>

                {/* Password */}
                <div>
                    <label className="block text-sm font-medium mb-1">Password</label>
                    <input
                        type="text"
                        {...register("password")}
                        className="w-full border rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
                        placeholder="Leave blank if no change"
                    />

                </div>

                {/* Submit Button */}
                <button onClick={()=>navigate("/dashboard")}
                    type="submit"
        className="w-full bg-blue-600 text-white py-2.5 rounded-xl 
        font-medium hover:bg-blue-700 transition active:scale-[0.98]"
                >
                    Update
                </button>
            </form>
        </div>
    )
}

export default CredentialUpdate
