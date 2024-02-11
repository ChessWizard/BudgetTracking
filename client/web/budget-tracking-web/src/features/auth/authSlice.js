import { createSlice } from "@reduxjs/toolkit";

const authSlice = createSlice({
  name: "auth",
  initialState: {
    token: null,
    accessTokenExpiration: null,
    refreshToken: null,
    refreshTokenExpiration: null
  },
  reducers: {
    setCredentials: (state, action) => {
      const {
        accessTokenExpiration,
        accessToken,
        refreshToken,
        refreshTokenExpiration,
      } = action.payload;
      state.token = accessToken;
      state.accessTokenExpiration = accessTokenExpiration;
      state.refreshToken = refreshToken;
      state.refreshTokenExpiration = refreshTokenExpiration;
    },
    logout: (state, action) => {
      state.accessTokenExpiration = null;
      state.token = null;
      state.refreshToken = null;
    },
  },
});

export const isTokenExpired = () => (dispatch, getState) => {
  const { auth } = getState();

  const accessTokenExpirationTime = auth.accessTokenExpiration;
  const isAccessTokenExpired = checkTokenExpired(accessTokenExpirationTime);
  const refreshTokenExpirationTime = auth.refreshTokenExpiration;
  const isRefreshTokenExpired = checkTokenExpired(refreshTokenExpirationTime);

  // her ikisinin de süresi dolduysa oturum kapatılır
  if (isAccessTokenExpired && isRefreshTokenExpired) {
    console.log("Token süresi doldu, localStorage'dan kaldırıldı.");
    return {access: true, refresh: true}
  }

  // yalnızca access token doluğ refresh token dolmadıysa
  // oturuma devam edebilmek için refresh token üzerinden yeni access token yaratılır
  if(isAccessTokenExpired && !isRefreshTokenExpired){
    console.log("Refresh token üzerinden access token yaratılıyor.")
    return {access: true, refresh: false}
  }

  // ikisi de doluysa zaten henüz ömrü vardır
    return {access: false, refresh: false}
};

const checkTokenExpired = (expirationTime) => {
  const expireDate = new Date(expirationTime);
  const currentDate = new Date();
  console.log("Token süresi: " + expireDate + " " + currentDate);
  return expireDate < currentDate;
};

export const logoutUser = () => (dispatch) => {
  dispatch(authSlice.actions.logout());
};

export const { setCredentials, logout } = authSlice.actions;

export default authSlice.reducer;

export const selectCurrentAccessTokenExpiration = (state) =>
  state.auth.accessTokenExpiration;
export const selectCurrentToken = (state) => state.auth.token;
export const selectCurrentRefreshToken = (state) => state.auth.refreshToken;
