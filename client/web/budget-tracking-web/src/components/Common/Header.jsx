import React, { useState } from "react";
import { RxHamburgerMenu } from "react-icons/rx";
import { FaMoneyBill1Wave } from "react-icons/fa6";
import { IoCalendar } from "react-icons/io5";
import { IoPieChart } from "react-icons/io5";
import { IoIosAddCircle } from "react-icons/io";
import { FaMoneyBillTrendUp } from "react-icons/fa6";
import { FaUser } from "react-icons/fa";
import { getIconByLabel, getNotLoggedInIconByLabel } from "../../helpers/IconHelper";
import { menuItems, notLoggedInMenuItems } from "../../constants/IconConstants";

const Header = () => {
  const [isHamburgerMenuActive, setHamburgerMenuActive] = useState(false);
  const token = "34564634";

  const handleMenuClick = () => {
    setHamburgerMenuActive(!isHamburgerMenuActive);
  };

  return (
    <>
      <div className="header-wrapper container-md p-5 md:p-8 lg:p-8 bg-red-500">
        {/* Mobile Hamburger Menu Start */}
        <span
          onClick={() => handleMenuClick()}
          id="guide-menu"
          className="inline md:hidden lg:hidden"
        >
          <RxHamburgerMenu
            // style={{ marginLeft: "2rem", marginTop: "2rem" }}
            size={"30"}
            color="white"
            style={{cursor:"pointer"}}
          />
        </span>
        {token === "" ? (
          <>
          <span className="hidden md:inline lg:inline">
              <div className="flex">
                <FaMoneyBillTrendUp size={"45"} color="white"/>
                <div className="text-base font-semibold mt-4 ml-5 text-fuchsia-50">
                  Etkili Bütçe Takibi
                </div>
                <div
                  style={{ color: "#f3796e", cursor: "pointer" }}
                  className="flex items-center ml-auto mr-3 bg-white p-3 rounded-xl font-bold"
                >
                  <div className="mr-2">
                    <FaUser size={"20"} color="#f3796e" />
                  </div>
                  <div>Giriş Yap</div>
                </div>
              </div>
            </span>
            {isHamburgerMenuActive && (
              <div className="guide-menu-content p-3 mt-3 absolute bg-emerald-600 md:hidden lg:hidden rounded">
                <div className="flex">
                  <FaMoneyBillTrendUp
                    size={"25"}
                    style={{ marginBottom: "1rem", marginRight: "1rem" }}
                    color="white"
                  />
                  <div className="text-base font-semibold mt-1 text-fuchsia-50">
                    Etkili Bütçe Takibi
                  </div>
                  {/* Site icon */}
                </div>
                {notLoggedInMenuItems.map((item) => (
                  <>
                    <div
                      style={{ cursor: "pointer" }}
                      className="flex items-center my-2 p-2 hover:bg-black hover:rounded"
                    >
                      <span className="mx-3">{getNotLoggedInIconByLabel(item.label)}</span>
                      <span className="text-fuchsia-50">{item.label}</span>
                    </div>
                  </>
                ))}
              </div>
            )}
          </>
        ) : (
          <>
            <span className="hidden md:inline lg:inline">
              <div className="flex">
                <FaMoneyBillTrendUp size={"45"} color="white" />
                <div className="text-base font-semibold mt-4 ml-5 text-fuchsia-50">
                  Etkili Bütçe Takibi
                </div>
                <div
                  style={{ color: "#f3796e", cursor: "pointer" }}
                  className="flex items-center ml-auto mr-3 bg-white p-3 rounded-xl font-bold"
                >
                  <div className="mr-2">
                    <FaUser size={"20"} color="#f3796e" />
                  </div>
                  <div>Hesabım</div>
                </div>
                <div
                  style={{ color: "#f3796e", cursor: "pointer" }}
                  className="flex items-center mr-3 bg-white p-3 rounded-xl font-bold"
                >
                  <div className="mr-2">
                    <FaMoneyBill1Wave size={"20"} color="#f3796e" />
                  </div>
                  <div>Harcamalarım</div>
                </div>
                <div
                  style={{ color: "#f3796e", cursor: "pointer" }}
                  className="flex items-center mr-3 bg-white p-3 rounded-xl font-bold"
                >
                  <div className="mr-2">
                    <IoCalendar size={"20"} color="#f3796e" />
                  </div>
                  <div>Takvim</div>
                </div>
                <div
                  style={{ color: "#f3796e", cursor: "pointer" }}
                  className="flex items-center mr-3 bg-white p-3 rounded-xl font-bold"
                >
                  <div className="mr-2">
                    <IoPieChart size={"20"} color="#f3796e" />
                  </div>
                  <div>Grafik</div>
                </div>
              </div>
            </span>
            {isHamburgerMenuActive && (
              <div className="guide-menu-content p-3 mt-3 absolute bg-emerald-600 md:hidden lg:hidden rounded">
                <div className="flex">
                  <FaMoneyBillTrendUp
                    size={"25"}
                    style={{ marginBottom: "1rem", marginRight: "1rem" }}
                    color="white"
                  />
                  <div className="text-base font-semibold mt-1 text-fuchsia-50">
                  Etkili Bütçe Takibi
                  </div>
                  {/* Site icon */}
                </div>

                {menuItems.map((item) => (
                  <>
                    <div
                      style={{ cursor: "pointer" }}
                      className="flex items-center my-2 p-2 hover:bg-black hover:rounded"
                    >
                      <span className="mx-3">{getIconByLabel(item.label)}</span>
                      <span className="text-fuchsia-50">{item.label}</span>
                    </div>
                  </>
                ))}
              </div>
            )}
            {/* Mobile Hamburger Menu End */}
          </>
        )}
      </div>
    </>
  );
};

export default Header;
