import React, { useState } from "react";
import { MdEmail } from "react-icons/md";
import { RiLockPasswordFill } from "react-icons/ri";
import { FaMoneyBillTrendUp } from "react-icons/fa6";
import { Link, useNavigate } from "react-router-dom";
import { FaEye } from "react-icons/fa";
import { FaEyeSlash } from "react-icons/fa";
import { login, useLoginMutation } from "../features/auth/authApiSlice";
import { useDispatch } from "react-redux";
import { setCredentials } from "../features/auth/authSlice";
import { useForm } from "react-hook-form";

const Login = () => {
  const navigate = useNavigate();
  const [isPasswordVisible, setPasswordVisible] = useState(false);
  const dispatch = useDispatch();
  const [login, {isLoading}] = useLoginMutation()

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const loginUser = async (data) => {
    console.log("clicked")
    try {
      var email = data.email;
      var password = data.password;

      const userData = await login({ email, password }).unwrap();
      console.log(userData);

      dispatch(setCredentials({ ...userData.data }));
      navigate("/");
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      <div className="bg-violet-700 w-screen h-screen flex justify-center items-center flex-column ">
        <div className="mb-20">
          <div
            style={{ cursor: "pointer" }}
            className="flex justify-center items-center"
            onClick={() => navigate("/")}
          >
            <FaMoneyBillTrendUp
              size={"100"}
              color="white"
              className="mb-10 w-1/4 md:w-max lg:w-max"
            />
            <div className="text-xl text-white ml-5 md:text-2xl lg:text-3xl">
              Etkili Bütçe
            </div>
          </div>
          <form
            id="login-card"
            className="bg-white shadow p-5 rounded w-full md:w-max lg:w-max md:p-12 lg:p-14"
            onSubmit={handleSubmit((data) => loginUser(data))}
          >
            <div className="flex items-end justify-center mb-3">
              <MdEmail size={"25"} className="mb-1" />
              <div>
                <div className="text-dark mb-1 ml-3">E-posta</div>
                <input
                  type="email"
                  size={"30"}
                  className="border-solid border-2 border-black w-full ml-3 pl-2 pr-2 pt-1 pb-1 rounded-xl 
                    md:pl-4 md:pr-16 lg:pl-4 lg:pr-20"
                  {...register("email")}
                />
              </div>
            </div>
            <div className="flex items-end justify-center">
              <RiLockPasswordFill size={"25"} className="mb-1" />
              <div>
                <div className="text-dark mb-1 ml-3">Şifre</div>
                <input
                  type={isPasswordVisible ? "text" : "password"}
                  size={"30"}
                  className="border-solid border-2 border-black w-full ml-3 pl-2 pr-2 pt-1 pb-1 rounded-xl 
                    md:pl-4 md:pr-16 lg:pl-4 lg:pr-20"
                  {...register("password")}
                />
              </div>
              <div
                id="password-visibility-container"
                onClick={() => setPasswordVisible(!isPasswordVisible)}
              >
                {isPasswordVisible ? (
                  <FaEye
                    size={"30"}
                    className="mt-1 mb-1"
                    style={{ marginLeft: "-2rem", cursor: "pointer" }}
                  />
                ) : (
                  <FaEyeSlash
                    size={"30"}
                    className="mt-1 mb-1"
                    style={{ marginLeft: "-2rem", cursor: "pointer" }}
                  />
                )}
              </div>
            </div>
            <div className="flex justify-center">
              <button
                type="submit"
                className="w-full text-center mt-5 p-2 text-white font-semibold rounded-xl bg-[#f3796e]"
              >
                Giriş Yap
              </button>
            </div>
          </form>
          <Link
            to={"/register"}
            className="block text-sm text-white text-center mt-2 md:text-base lg:text-base"
          >
            Henüz üye değil misiniz? Üye olun.
          </Link>
        </div>
      </div>
    </>
  );
};

export default Login;
