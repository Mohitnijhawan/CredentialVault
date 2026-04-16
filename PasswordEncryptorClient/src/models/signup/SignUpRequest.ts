export interface SignUpRequest
{
    email:string,
    password:string,
    contactNo:string
}

export interface SignUpResponse
{
    id:string,
    email:string,
    contactNo:string
}