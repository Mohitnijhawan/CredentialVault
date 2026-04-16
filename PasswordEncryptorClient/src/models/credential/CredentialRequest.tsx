export interface CredentialRequest
{
    title:string,
    userName:string,
    password:string
}

export interface CredentialResponse
{
    id:string
    title:string,
    username:string,
    encryptedPassword:string,
    createdAt:Date
}

export interface CredentialUpdateRequest
{
    id:string
    title:string,
    username:string,
    password?:string
}
