import type { LoginRequest, LoginResponse } from "../../models/login/LoginRequest";
import type { RefreshTokenRequest, RefreshTokenResponse } from "../../models/refreshToken/refreshToken";
import type { SignUpRequest, SignUpResponse } from "../../models/signup/SignUpRequest";
import { apiClient } from "../lib/axios";
import type { Result } from "../utilis/result";

export const SignUp=async(model:SignUpRequest):Promise<Result<SignUpResponse>>=>{

    return (await apiClient.post<Result<SignUpResponse>>("auth/sign-up",model))?.data;
}

export const Login=async(model:LoginRequest):Promise<Result<LoginResponse>>=>{
    return (await apiClient.post<Result<LoginResponse>>("auth/login",model))?.data;
}

export const refreshTokenlogin=async(model:RefreshTokenRequest):Promise<Result<RefreshTokenResponse>>=>{
    return (await apiClient.post<Result<RefreshTokenResponse>>("auth/refresh-token",model))?.data;
}

export const logout=async():Promise<Result<string>>=>{
    return (await apiClient.post<Result<string>>("auth/logout"))?.data;
}