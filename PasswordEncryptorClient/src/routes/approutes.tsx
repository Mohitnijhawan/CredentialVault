import { Navigate, Route, Routes } from "react-router-dom"
import Signup from "../pages/auth/signup"
import DashboardLayout from "../layout/dashboard-layout/dashboard-layout"
import GetCredentials from "../pages/credential/GetCredentials"
import CreateCredential from "../pages/credential/CreateCredential"
import CredentialUpdate from "../pages/credential/CredentialUpdate"
import LoginPage from "../pages/auth/login"

const AppRotes = () => {
  return (
    <Routes>
    <Route path="/" element={<Navigate to ="/signup"/>}/>
      <Route path="/signup" element={<Signup/>} />
      <Route path="/login" element={<LoginPage/>} />

 <Route path="/dashboard" element={<DashboardLayout />}>
          <Route index element={<GetCredentials />} />
          <Route path="add" element={<CreateCredential />} />
          <Route path="edit/:id" element={<CredentialUpdate />} />
        </Route>



    </Routes>
  )

}

export default AppRotes
