import { useEffect, useState } from "react";
import type { CredentialResponse } from "../../models/credential/CredentialRequest";
import {
  delteCredential,
  filterCredential,
  getCredentials,
  revealPassword,
} from "../../core/services/credentialService";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

const GetCredentials = () => {
  const [credentials, SetCredentials] = useState<CredentialResponse[]>([]);
  const [openId, setOpenId] = useState<string | null>(null);
  const [realPassword, setRealPassword] = useState<string>("");

  const [username, setUsername] = useState("");
  const [title, setTitle] = useState("");

  const [loading, setLoading] = useState(false);
  const [noResult, setNoResult] = useState(false);

  const navigate = useNavigate();

  const fetchCredentials = async () => {
    setLoading(true);
    const data = await getCredentials();
    if (data.isSuccess) {
      SetCredentials(data.data);
    }
    setLoading(false);
  };

  useEffect(() => {
    fetchCredentials();
  }, []);

  // 🔥 FILTER
  const handleFilter = async (u?: string, t?: string) => {
    setLoading(true);

    if (!u && !t) {
      await fetchCredentials();
      setNoResult(false);
      return;
    }

    const res = await filterCredential(u, t);

    if (res.isSuccess) {
      SetCredentials(res.data);

      // 👇 important
      setNoResult(res.data.length === 0);
    }

    setLoading(false);
  };

  const DeleteCredential = async (id: string) => {
    const data = await delteCredential(id);
    if (data.isSuccess) {
      toast.success(data.message);
      await fetchCredentials();
    }
  };

  const handleToggle = async (id: string) => {
    if (openId === id) {
      setOpenId(null);
      setRealPassword("");
    } else {
      const res = await revealPassword(id);
      if (res.isSuccess) {
        setOpenId(id);
        setRealPassword(res.data);
      }
    }
  };

  return (
   <div className="w-full">

  {/* Header */}
  <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 mb-6">
    <h1 className="text-2xl font-semibold text-gray-800">
      Credentials
    </h1>

    <button
      onClick={() => navigate("/dashboard/add")}
      className="bg-blue-600 text-white px-4 py-2 rounded-lg text-sm hover:bg-blue-700 transition"
    >
      + Add Credential
    </button>
  </div>

  {/* 🔍 Search */}
  <div className="grid md:grid-cols-2 gap-4 mb-2">
    <input
      type="text"
      placeholder="Search by username..."
      value={username}
      onChange={(e) => {
        const val = e.target.value;
        setUsername(val);
        handleFilter(val, title);
      }}
      className="border border-gray-200 rounded-lg px-4 py-2 text-sm focus:ring-2 focus:ring-blue-500 outline-none"
    />

    <input
      type="text"
      placeholder="Search by title..."
      value={title}
      onChange={(e) => {
        const val = e.target.value;
        setTitle(val);
        handleFilter(username, val);
      }}
      className="border border-gray-200 rounded-lg px-4 py-2 text-sm focus:ring-2 focus:ring-blue-500 outline-none"
    />
  </div>

  {/* 🔥 Loader */}
  {loading ? (
    <div className="flex justify-center py-10">
      <div className="w-8 h-8 border-4 border-blue-500 border-t-transparent rounded-full animate-spin"></div>
    </div>
  ) : credentials.length === 0 ? (
    <div className="text-center py-10 text-gray-500">
      No credentials found 😔
    </div>
  ) : (
    <>
      {/* 🖥 DESKTOP TABLE */}
      <div className="hidden md:block bg-white border border-gray-200 rounded-xl overflow-hidden">

        <table className="w-full text-sm">

          <thead className="bg-gray-50 text-gray-500 text-xs uppercase">
            <tr>
              <th className="px-6 py-3 text-left">Title</th>
              <th className="px-6 py-3 text-left">Username</th>
              <th className="px-6 py-3 text-left">Password</th>
              <th className="px-6 py-3 text-left">Created</th>
              <th className="px-6 py-3 text-right">Actions</th>
            </tr>
          </thead>

          <tbody>
            {credentials.map((c) => (
              <tr key={c.id} className="border-t hover:bg-gray-50">

                <td className="px-6 py-4 font-medium text-gray-800">
                  {c.title}
                </td>

                <td className="px-6 py-4 text-gray-600 truncate max-w-[180px]">
                  {c.username}
                </td>

                <td className="px-6 py-4 text-gray-600 truncate max-w-[200px]">
                  {openId === c.id ? realPassword : c.encryptedPassword}
                </td>

                <td className="px-6 py-4 text-gray-500 text-xs whitespace-nowrap">
                  {new Date(c.createdAt).toLocaleString()}
                </td>

                <td className="px-6 py-4">
                  <div className="flex justify-end gap-3">

                    <button
                      onClick={() => handleToggle(c.id)}
                      className="text-gray-500 hover:text-black text-xs"
                    >
                      {openId === c.id ? "Hide" : "Show"}
                    </button>

                    <button
                      onClick={() => navigate("/dashboard/edit/" + c.id)}
                      className="text-blue-600 hover:text-blue-800 text-xs"
                    >
                      Edit
                    </button>

                    <button
                      onClick={() => DeleteCredential(c.id)}
                      className="text-red-500 hover:text-red-700 text-xs"
                    >
                      Delete
                    </button>

                  </div>
                </td>

              </tr>
            ))}
          </tbody>

        </table>
      </div>

      {/* 📱 MOBILE CARD VIEW */}
      <div className="md:hidden flex flex-col gap-4 mt-4">
        {credentials.map((c) => (
          <div
            key={c.id}
            className="bg-white p-4 rounded-xl border border-gray-200 shadow-sm"
          >

            <h2 className="font-semibold text-gray-800 mb-2">
              {c.title}
            </h2>

            <p className="text-sm text-gray-600">
              <span className="font-medium">Username:</span> {c.username}
            </p>

            <p className="text-sm text-gray-600 break-all">
              <span className="font-medium">Password:</span>{" "}
              {openId === c.id ? realPassword : c.encryptedPassword}
            </p>

            <p className="text-xs text-gray-500 mt-1">
              {new Date(c.createdAt).toLocaleString()}
            </p>

            {/* 🔥 Buttons visible always */}
            <div className="flex gap-3 mt-3 flex-wrap">

              <button
                onClick={() => handleToggle(c.id)}
                className="px-3 py-1 text-xs rounded-lg bg-gray-100"
              >
                {openId === c.id ? "Hide" : "Show"}
              </button>

              <button
                onClick={() => navigate("/dashboard/edit/" + c.id)}
                className="px-3 py-1 text-xs rounded-lg bg-blue-100 text-blue-600"
              >
                Edit
              </button>

              <button
                onClick={() => DeleteCredential(c.id)}
                className="px-3 py-1 text-xs rounded-lg bg-red-100 text-red-600"
              >
                Delete
              </button>

            </div>

          </div>
        ))}
      </div>
    </>
  )}

</div>
  );
};

export default GetCredentials;