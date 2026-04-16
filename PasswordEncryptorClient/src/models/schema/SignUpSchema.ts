import z from "zod";

export const SignUpSchema=z.object({
    email:z.string().nonempty("Email is Required"),
    password:z.string().nonempty("Password is Required"),
    contactNo:z.string().nonempty("ContactNo is Required")
})