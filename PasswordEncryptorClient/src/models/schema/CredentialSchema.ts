import z from "zod";

export const CredentialRequestSchema=z.object({

    title:z.string().nonempty("Title is Required"),
    userName:z.string().nonempty("UserName is Required"),
    password:z.string().nonempty("Password is Required"),

})

export const CredentialUpdateSchema=z.object({
    id:z.string().nonempty(),
    title:z.string().nonempty("Title is Required"),
    username:z.string().nonempty("UserName is Required"),
    encrypedPassword:z.string().nonempty().optional(),

})