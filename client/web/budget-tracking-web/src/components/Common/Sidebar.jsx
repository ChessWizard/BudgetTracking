/* eslint-disable jsx-a11y/alt-text */
import React, { useState } from "react";
import { FaMoneyBillTrendUp } from "react-icons/fa6";
import { LuChevronFirst, LuChevronLast } from "react-icons/lu";
import { getSidebarIconsByLabel } from "../../helpers/IconHelper";

const Sidebar = () => {
    const [isExpanded, setExpanded] = useState(false);

    const sidebarElements = [
      {
        label: "Genel Bakış",
      },
      {
        label: "İşlemler",
      },
      {
        label: "Harcamalarım",
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
  
    //dummy
    const fullName = "John+Doe";
    const email = "johndoe@gmail.com";
  
    return (
      <>
        <aside className="h-screen w-max">
          <nav style={{backgroundColor:"#EF4444"}} className="h-full w-max flex flex-col items-start bg-white border border-solid border-[#64748b] shadow-lg">
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
              overflow-hidden transition-all ${!isExpanded ? "block" : "hidden"}`}
              >
                {fullName.split("+").join(" ")}
                <br />
                <span className="text-base leading-4">{email}</span>
              </div>
            </div>
          </nav>
        </aside>
      </>
    );
}

export default Sidebar
