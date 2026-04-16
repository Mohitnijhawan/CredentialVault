import type { CredentialRequest, CredentialResponse, CredentialUpdateRequest } from "../../models/credential/CredentialRequest";
import { apiClient } from "../lib/axios";
import type { Result } from "../utilis/result";

export const createCredential=async(model:CredentialRequest):Promise<Result<CredentialResponse>>=>{
    return (await apiClient.post<Result<CredentialResponse>>("credentials",model))?.data;
}

export const getCredentials=async():Promise<Result<CredentialResponse[]>>=>{
    return (await apiClient.get<Result<CredentialResponse[]>>("credentials"))?.data;
}

export const getCredentialById=async(id:string):Promise<Result<CredentialResponse>>=>{
    return (await apiClient.get<Result<CredentialResponse>>(`credentials/${id}`))?.data;
}

export const updateCredential=async(model:CredentialUpdateRequest):Promise<Result<CredentialResponse>>=>{
    return (await apiClient.put<Result<CredentialResponse>>("credentials",model))?.data;
}

export const delteCredential=async(id:string):Promise<Result<CredentialResponse>>=>{
    return (await apiClient.delete<Result<CredentialResponse>>(`credentials/${id}`))?.data;
}

export const revealPassword=async(id:string):Promise<Result<string>>=>{
    return (await apiClient.get<Result<string>>(`credentials/reveal/${id}`))?.data;
}

export const filterCredential=async(username?:string,title?:string):Promise<Result<CredentialResponse[]>>=>{
    return (await apiClient.get<Result<CredentialResponse[]>>(`credentials/search?username=${username}&title=${title}`))?.data;
}