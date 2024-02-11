import React from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  isTokenExpired,
  logoutUser,
  selectCurrentRefreshToken,
  selectCurrentToken,
  setCredentials,
} from "../../features/auth/authSlice";
import { Navigate } from "react-router-dom";
import { useLogoutMutation, useRefreshMutation } from "../../features/auth/authApiSlice";

const RequireAuth = ({ children }) => {
  const accessToken = useSelector(selectCurrentToken);
  const dispatch = useDispatch();
  const refreshToken = useSelector(selectCurrentRefreshToken);
  const [logout] = useLogoutMutation();
  const [refresh] = useRefreshMutation()

  // kullanıcı giriş yapmadıysa token bilgisi de null'dır
  // bundan dolayı doğrudan login'e gider kısıtlı yerlere ulaşmaya çalışırsa
  if (accessToken === null) return <Navigate to="/login" />;

  const isExpired = dispatch(isTokenExpired());
  console.log(isExpired)

  // eğer accessToken'ı local storage üzerinde varsa, yani önceden giriş yaptıysa
  // access token'ının ömrünün olup olmadığına bakılır
  // refresh token süresine de bakılır, o da bittiyse artık ölüdür
  // ömrü yoksa logout edilir, varsa istenilen içeriğe ulaşır
  if (isExpired.access && isExpired.refresh) {
    logout(refreshToken);
    dispatch(logoutUser());
    return <Navigate to="/login" />;
  }

  // eğer access token bittiyse ama refresh token'ın ömrü devam ediyorsa
  // refresh token üzerinden yeni access token üretilerek token'ımız
  // ve dolyısıyla oturumumuz tazelenir
  if(isExpired.access && !isExpired.refresh){
    const userData = refresh(refreshToken)
    dispatch(setCredentials({ ...userData.data }));
    return children
  }

  // tüm hatalı durumlar kontrol edilince artık istenilen içeriğe ulaşılabilir
  return children;
};

export default RequireAuth;
