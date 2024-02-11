import {createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';
import { setCredentials, logout  } from '../../features/auth/authSlice';

const baseQuery = fetchBaseQuery({
    baseUrl: `https://budgettracking77.azurewebsites.net/api`,
    credentials: 'include',
    prepareHeaders: (headers, {getState}) => {
        const token = getState().auth.token;
        console.log('token: ' + token);
        if(token){
            headers.set('Authorization', `Bearer ${token}`);
        }
        return headers;
    }
})

const baseQueryWithReauth = async (args, api, extraOptions) => {
    let result = await baseQuery(args, api, extraOptions);

    if(result?.error?.originalStatus === 403){
        console.log('refresh token gönderiliyor');
        const refreshTokenResult = await baseQuery('/refreshSession', api, extraOptions);
        if(refreshTokenResult.data){
            const user = api.getState().auth.user;

            api.dispatch(setCredentials({...refreshTokenResult, user}));

            result = await baseQuery(args, api, extraOptions);
        }
        else{
            api.dispatch(logout());
        }
    }
    return result;
}

export const apiSlice = createApi({
    baseQuery: baseQueryWithReauth,
    endpoints: builder => ({})
})