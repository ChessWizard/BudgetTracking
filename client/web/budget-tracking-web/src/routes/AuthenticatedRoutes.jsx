import React from "react";
import { Navigate, Route, Routes } from "react-router-dom";

const AuthenticatedRoutes = ({ isAuthenticated, ...rest }) => {
  return (
    <>
      <Routes>
        <Route
          {...rest}
          element={!isAuthenticated && <Navigate to="/login" />}
        ></Route>
      </Routes>
    </>
  );
};

export default AuthenticatedRoutes;
