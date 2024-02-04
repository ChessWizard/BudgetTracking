import React, { useState } from "react";
import { MdEmail } from "react-icons/md";
import { RiLockPasswordFill } from "react-icons/ri";
import { FaMoneyBillTrendUp } from "react-icons/fa6";
import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import axios from "axios";
import { FaEye } from "react-icons/fa";
import { FaEyeSlash } from "react-icons/fa";

const Register = () => {
  const navigate = useNavigate();
  const [isPasswordVisible, setPasswordVisible] = useState(false);
  const [isRetryPasswordVisible, setRetryPasswordVisible] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    defaultValues: {
      email: "",
      password: "",
      retryPassword: "",
    },
  });

  const registerUser = async (data) => {
    await axios
      .post(`https://budgettracking77.azurewebsites.net/api/Users/register`, data)
      .then((response) => {
        // setTimeout(() => {
        //   setLoading(false);
        // }, 100);
        // // 200 ve türevlerinde buralara düşer
        // toast.success(response.data.message);
        // navigate("/login")
        console.log(response);
      })
      .catch((error) => {
        // setTimeout(() => {
        //   setLoading(false);
        // }, 100);
        // // 200 dışındakiler catch bloğuna düşer
        // if (parseInt(error.response.data.httpStatusCode) === 400) {
        //   toast.error(error.response.data.errorDto.errors[0]);
        // }
      });
  };

  return (
    <>
      <div className="bg-emerald-600 w-screen h-screen flex justify-center items-center flex-column">
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
            onSubmit={handleSubmit((data) => registerUser(data))}
          >
            <div className="flex items-end justify-center mb-3 mr-3">
              <MdEmail size={"25"} className="mb-1" />
              <div>
                <div className="text-dark mb-1 ml-3">E-posta</div>
                <input
                  type="email"
                  size={"30"}
                  {...register("email")}
                  className="border-solid border-2 border-black w-full ml-3 pl-2 pr-2 pt-1 pb-1 rounded-xl 
                    md:pl-4 md:pr-16 lg:pl-4 lg:pr-20"
                  style={{ marginRight: "-1.2rem" }}
                />
              </div>
            </div>
            <div className="flex items-end justify-center mb-3">
              <RiLockPasswordFill size={"25"} className="mb-1" />
              <div>
                <div className="text-dark mb-1 ml-3">Şifre</div>
                <div className="flex">
                  <input
                    type={isPasswordVisible ? "text" : "password"}
                    size={"30"}
                    {...register("password")}
                    className="border-solid border-2 border-black w-full ml-3 pl-2 pr-2 pt-1 pb-1 rounded-xl 
                    md:pl-4 md:pr-16 lg:pl-4 lg:pr-20"
                  />
                  <div
                    id="password-visibility-container"
                    onClick={() => setPasswordVisible(!isPasswordVisible)}
                  >
                    {isPasswordVisible ? (
                      <FaEye
                        size={"30"}
                        className="mt-1"
                        style={{ marginLeft: "-2.5rem", cursor: "pointer" }}
                      />
                    ) : (
                      <FaEyeSlash
                        size={"30"}
                        className="mt-1"
                        style={{ marginLeft: "-2.5rem", cursor: "pointer" }}
                      />
                    )}
                  </div>
                </div>
              </div>
            </div>
            <div className="flex items-end justify-center">
              <RiLockPasswordFill size={"25"} className="mb-1" />
              <div>
                <div className="text-dark mb-1 ml-3">Şifre Tekrar</div>
                <div className="flex">
                  <input
                    type={isRetryPasswordVisible ? "text" : "password"}
                    size={"30"}
                    {...register("retryPassword")}
                    className="border-solid border-2 border-black w-full ml-3 pl-2 pr-2 pt-1 pb-1 rounded-xl 
                    md:pl-4 md:pr-16 lg:pl-4 lg:pr-20"
                  />
                  <div
                    id="retryPassword-visibility-container"
                    onClick={() =>
                      setRetryPasswordVisible(!isRetryPasswordVisible)
                    }
                  >
                    {isRetryPasswordVisible ? (
                      <FaEye
                        size={"30"}
                        className="mt-1"
                        style={{ marginLeft: "-2.5rem", cursor: "pointer" }}
                      />
                    ) : (
                      <FaEyeSlash
                        size={"30"}
                        className="mt-1"
                        style={{ marginLeft: "-2.5rem", cursor: "pointer" }}
                      />
                    )}
                  </div>
                </div>
              </div>
            </div>
            <div className="flex justify-center">
              <button
                type="submit"
                className="w-full text-center mt-5 p-2 text-white font-semibold rounded-xl bg-[#f3796e] hover:bg-[#EF3B3B]"
              >
                Kayıt Ol
              </button>
            </div>
          </form>
          <Link
            to={"/login"}
            className="block text-sm text-white text-center mt-2 md:text-base lg:text-base"
          >
            Zaten üye misiniz? Giriş Yapın.
          </Link>
        </div>
      </div>
    </>
  );
};

export default Register;
