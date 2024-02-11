/* eslint-disable jsx-a11y/alt-text */
import React, { useEffect, useState } from "react";
import { FaMoneyBillTrendUp } from "react-icons/fa6";
import { LuChevronFirst, LuChevronLast } from "react-icons/lu";
import { getSidebarIconsByLabel, getSidebarRouteByLabel } from "../../helpers/IconHelper";
import { MdLogout } from "react-icons/md";
import { FaUser } from "react-icons/fa";
import axios from "axios";
import { useDispatch, useSelector } from "react-redux";
import {
  logoutUser,
  selectCurrentRefreshToken,
  selectCurrentToken,
} from "../../features/auth/authSlice";
import { useLogoutMutation } from "../../features/auth/authApiSlice";
import { useNavigate } from "react-router-dom";

const Sidebar = () => {
  const [isExpanded, setExpanded] = useState(false);
  const [userDetails, setUserDetails] = useState(null);
  const [fullName, setFullName] = useState("İsimsiz Kullanıcı");
  const [isLoading, setLoading] = useState(true);
  const navigate = useNavigate()

  const sidebarElements = [
    {
      label: "Genel Bakış",
    },
    {
      label: "İşlemler",
    },
    {
      label: "Hesaplar",
    },
    {
      label: "Planlanmış Ödemeler",
    },
    {
      label: "Bütçeler",
    },
    {
      label: "Grafikler",
    },
    {
      label: "Takvim",
    },
  ];

  const accessToken = useSelector(selectCurrentToken);
  const getUserDetails = () => {
    axios
      .get("https://budgettracking77.azurewebsites.net/api/Users", {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then((response) => {
        setUserDetails(response.data.data);
        if (
          response.data.data.name !== undefined &&
          response.data.data.surname !== undefined
        ) {
          setFullName(
            `${response.data.data.name}+${response.data.data.surname}`
          );
        }
      })
      .catch((error) => {
        console.log(error);
      })
      // herhangi bir süreye bağımlı olmadan response döndüğünde loading false olsun
      .finally(() => {
        setLoading(false)
      })
  };

  const dispatch = useDispatch();
  const refreshToken = useSelector(selectCurrentRefreshToken);
  const [logout] = useLogoutMutation();

  const handleLogout = () => {
    logout(refreshToken);
    dispatch(logoutUser());
  };

  useEffect(() => {
    getUserDetails();
  }, []);

  return (
    <>
      {!isLoading && (
        <aside className="h-screen w-max">
          <nav
            style={{ backgroundColor: "#EF4444" }}
            className={`h-full w-max flex flex-col items-start bg-white border border-solid border-[#64748b] shadow-lg 
          ${!isExpanded && "absolute"}
          md:static lg:static`}
          >
            <div className="p-4 pb-2 pt-8 flex justify-around items-center w-max">
              <div
                className={`flex overflow-hidden transition-all ${
                  !isExpanded ? "block" : "hidden"
                }`}
              >
                <FaMoneyBillTrendUp size={"60"} color="#FFFFFF" />
                <div className="text-white text-xl font-semibold mt-auto ml-3 mr-5">
                  Etkili Bütçe
                </div>
              </div>

              <button
                className="p-1.5 ml-2 rounded-lg bg-gray-50 mt-auto hover:bg-gray-100
                md:p-2 lg:p-2"
                onClick={() => setExpanded(!isExpanded)}
              >
                {isExpanded ? (
                  <LuChevronLast size={"25"} />
                ) : (
                  <LuChevronFirst size={"25"} />
                )}
              </button>
            </div>
            <hr className="border border-solid border-1 w-full mt-4 mb-0" />
            <ul id="sidebar-elements-container" className="flex-1 py-3 w-full">
              {sidebarElements.map((item) => (
                <>
                  <li
                    style={{ cursor: "pointer" }}
                    className="flex p-3 px-5 items-center mx-2 rounded-xl mt-2 hover:bg-black
                    md:mt-4 lg:mt-6"
                    onClick={() => navigate(getSidebarRouteByLabel(item.label))}
                  >
                    <span>{getSidebarIconsByLabel(item.label)}</span>
                    <span
                      className={`text-white font-semibold px-4 overflow-hidden transition-all ${
                        !isExpanded ? "block" : "hidden"
                      }`}
                    >
                      {item.label}
                    </span>
                  </li>
                </>
              ))}
              <hr className="border border-solid border-1 w-full mt-4" />
            </ul>

            <div className="w-full mb-5">
              <div
                style={{ cursor: "pointer" }}
                className="flex p-3 px-5 items-center mx-2 rounded-xl mt-2 hover:bg-black
                    md:mt-4 lg:mt-6"
                onClick={() => navigate("/dashboard/account")}
              >
                <span>
                  <FaUser size={"25"} color="white" />
                </span>
                <span
                  className={`text-white font-semibold px-4 overflow-hidden transition-all ${
                    !isExpanded ? "block" : "hidden"
                  }`}
                >
                  Hesabım
                </span>
              </div>
              <div
                style={{ cursor: "pointer" }}
                className="flex p-3 px-5 items-center mx-2 rounded-xl mt-2 hover:bg-black
                    md:mt-4 lg:mt-6"
                onClick={() => handleLogout()}
              >
                <span>
                  <MdLogout size={"30"} color="white" />
                </span>
                <span
                  className={`text-white font-semibold px-4 overflow-hidden transition-all ${
                    !isExpanded ? "block" : "hidden"
                  }`}
                >
                  Çıkış Yap
                </span>
              </div>
            </div>
            <div
              id="profile-description-container"
              className="flex items-end w-full items-center"
            >
              <img
                src={`https://ui-avatars.com/api/?background=F0F0F0&color=f3796e&name=${fullName}`}
                className="rounded mx-3"
              />
              <div
                className={`text-xl text-white
              overflow-hidden transition-all ${
                !isExpanded ? "block" : "hidden"
              }`}
              >
                {fullName.split("+").join(" ")}
                <br />
                <span className="text-base leading-4">{userDetails.email}</span>
              </div>
            </div>
          </nav>
        </aside>
      )}
    </>
  );
};

export default Sidebar;
