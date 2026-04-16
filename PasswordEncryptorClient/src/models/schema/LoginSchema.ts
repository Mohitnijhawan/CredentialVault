import z from "zod";

export const LoginSchema=z.object({
    email:z.string().nonempty("Email is Required"),
    password:z.string().nonempty("Password is Required")
})