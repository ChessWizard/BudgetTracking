import { apiSlice } from "../../app/api/apiSlice";

export const authApiSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        login: builder.mutation({
            query: (credentials) => ({
                url: 'Auths/login',
                method: 'POST',
                body: {...credentials}
            })
        }),
        logout: builder.mutation({
            query: (refreshToken) => ({
                url: `Auths/logout?refreshToken=${refreshToken}`,
                method: 'DELETE',
            })
        }),
        refresh: builder.mutation({
            query: (refreshToken) => ({
                url: `Auths/refresh?refreshToken=${refreshToken}`,
                method: 'POST',
            })
        })
    })
})

export const { useLoginMutation, useLogoutMutation, useRefreshMutation } = authApiSlice;
export const { endpoints: { login, logout, refresh } } = authApiSlice;