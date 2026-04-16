import { Outlet, NavLink, useNavigate } from "react-router-dom";
import { useState } from "react";
import { logout } from "../../core/services/authService";
import { ACCESS_TOKEN_KEY, REFRESH_TOKEN_KEY } from "../../constants/storage-names";
import { toast } from "react-toastify";

const DashboardLayout = () => {
  const [open, setOpen] = useState(false);
  const navigate = useNavigate();

  const handleLogout = async () => {
    const res = await logout();
    if (res.isSuccess) {
      localStorage.removeItem(ACCESS_TOKEN_KEY);
      localStorage.removeItem(REFRESH_TOKEN_KEY);
      toast.success("Logged out");
      navigate("/login");
    }
  };

  return (
    <div className="h-screen flex bg-[#f7f8fa] overflow-hidden">

      {/* 🔥 Sidebar */}
      <aside
        className={`fixed md:static top-0 left-0 h-full w-64 bg-white border-r border-gray-200 flex flex-col z-50
        ${open ? "translate-x-0" : "-translate-x-full"} md:translate-x-0 transition`}
      >
        {/* Mobile Header */}
        <div className="flex justify-between items-center px-5 py-4 md:hidden border-b">
          <h2 className="font-semibold">Menu</h2>
          <button onClick={() => setOpen(false)}>✕</button>
        </div>

        {/* Logo */}
        <div className="px-6 py-6 border-b">
          <h1 className="text-lg font-semibold tracking-tight">
            🔐 Vault
          </h1>
        </div>

        {/* Nav */}
        <nav className="flex flex-col gap-1 p-3">

          <NavLink
            to="/dashboard"
            className={({ isActive }) =>
              `px-4 py-2.5 rounded-lg text-sm flex items-center gap-2 transition ${
                isActive
                  ? "bg-blue-50 text-blue-600 font-medium"
                  : "text-gray-600 hover:bg-gray-100"
              }`
            }
          >
            📂 Credentials
          </NavLink>

          <NavLink
            to="/dashboard/add"
            className={({ isActive }) =>
              `px-4 py-2.5 rounded-lg text-sm flex items-center gap-2 transition ${
                isActive
                  ? "bg-blue-50 text-blue-600 font-medium"
                  : "text-gray-600 hover:bg-gray-100"
              }`
            }
          >
            ➕ Add New
          </NavLink>

        </nav>

        {/* Logout */}
        <div className="mt-auto p-4 border-t">
          <button
            onClick={handleLogout}
            className="w-full text-sm text-red-500 hover:text-red-600"
          >
            Logout
          </button>
        </div>
      </aside>

      {/* 🔥 Overlay (mobile) */}
      {open && (
        <div
          onClick={() => setOpen(false)}
          className="fixed inset-0 bg-black/30 md:hidden"
        />
      )}

      {/* 🔥 Main (NO GAP FULL WIDTH) */}
      <main className="flex-1 h-full overflow-y-auto">

        {/* Mobile Topbar */}
        <div className="md:hidden flex items-center justify-between px-4 py-3 bg-white border-b">
          <h2 className="font-semibold">🔐 Vault</h2>
          <button onClick={() => setOpen(true)}>☰</button>
        </div>

        {/* Content */}
        <div className="p-4 md:p-8">
          <Outlet />
        </div>

      </main>
    </div>
  );
};

export default DashboardLayout;