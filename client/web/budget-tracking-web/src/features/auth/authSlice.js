import { createSlice } from "@reduxjs/toolkit";

const authSlice = createSlice({
  name: "auth",
  initialState: { accessTokenExpiration: null, token: null, refreshToken: null },
  reducers: {
    setCredentials: (state, action) => {
      const { accessTokenExpiration, accessToken, refreshToken } = action.payload;
      state.accessTokenExpiration = accessTokenExpiration;
      state.token = accessToken;
      state.refreshToken = refreshToken;
    },
    logout: (state, action) => {
      state.accessTokenExpiration = null;
      state.token = null;
      state.refreshToken = null;
    },
  },
});

export const checkAndRemoveExpiredToken = () => (dispatch, getState) => {
  const { auth } = getState();

  const expirationTime = auth.accessTokenExpiration;
  const isTokenExpired = isTokenExpiredFunc(expirationTime);

  if (isTokenExpired) {
    dispatch(authSlice.actions.logout());
    console.log("Token süresi doldu, localStorage'dan kaldırıldı.");
  }
};

const isTokenExpiredFunc = (expirationTime) => {
  const expireDate= new Date(expirationTime);
  const currentDate = new Date()
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
