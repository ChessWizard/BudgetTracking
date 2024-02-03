import React from "react";
import { MdEmail } from "react-icons/md";
import { RiLockPasswordFill } from "react-icons/ri";
import { FaMoneyBillTrendUp } from "react-icons/fa6";

const Login = () => {
  return (
    <>
      <div className="bg-violet-700 w-screen h-screen flex justify-center items-center flex-column ">
        <div className="mb-20">
          <div className="flex justify-center items-center">
            <FaMoneyBillTrendUp size={"100"} color="white" className="mb-10 w-1/4 md:w-max lg:w-max" />
            <div className="text-xl text-white ml-5 md:text-2xl lg:text-3xl">Etkili Bütçe</div>
          </div>
          <form
            id="login-card"
            className="bg-white shadow p-5 rounded w-full md:w-max lg:w-max md:p-12 lg:p-14"
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
                />
              </div>
            </div>
            <div className="flex items-end justify-center">
              <RiLockPasswordFill size={"25"} className="mb-1" />
              <div>
                <div className="text-dark mb-1 ml-3">Şifre</div>
                <input
                  type="password"
                  size={"30"}
                  className="border-solid border-2 border-black w-full ml-3 pl-2 pr-2 pt-1 pb-1 rounded-xl 
                    md:pl-4 md:pr-16 lg:pl-4 lg:pr-20"
                />
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
        </div>
      </div>
    </>
  );
};

export default Login;
