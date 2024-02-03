/* eslint-disable jsx-a11y/alt-text */
import React from "react";
import { IoIosAddCircle } from "react-icons/io";

const Home = () => {

  const handleHover = (isHovered) => {
    if(isHovered){
      document.getElementById("add-btn").style.color = "#f3796e"
      document.getElementById("budget-txt").style.color = "#f3796e"
    }
    else{
      document.getElementById("add-btn").style.color = "#ffffff"
      document.getElementById("budget-txt").style.color = "#ffffff"
    }
  }

  return (
    <>
      <div
        id="budget-tracing-layer"
        style={{ backgroundColor: "#d5e7f5" }}
        className="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-3 p-5 md:p-14 lg:p-14"
      >
        <div className="flex items-center justify-center md:justify-end lg:justify-end">
          <div
            style={{ color: "#f3796e" }}
            className="text-2xl font-semibold text-center mt-4 p-5 md:p-2 lg:p-2 md:ml-5 lg:ml-5 md:text-5xl lg:text-5xl"
          >
            Etkili Bütçe Takibi
          </div>
        </div>
        <div className="flex justify-center mt-5 p-2 md:p-2 lg:p-2 md:ml-5 lg:ml-5 ">
          <img
            className="rounded-xl"
            src="https://www.daniaaccounting.com/wp-content/uploads/2022/09/Budget.jpg?x82588"
          />
        </div>
        <div
          className="flex items-center text-base text-center 
                        md:p-2 lg:p-2 md:text-2xl lg:text-2xl
                        md:mr-5 lg:mr-5"
        >
          <div style={{ color: "#f3796e" }}>
            Başarılı bütçe takip programları ve güncel harcamalarınızın
            değerlendirmesi için bizi tercih edin!
          </div>
        </div>
      </div>
      <div
        id="budget-add-layer"
        style={{ backgroundColor: "#77b4c7" }}
        className="grid grid-cols-none text-center p-5 md:grid-cols-2 lg:grid-cols-2 md:p-20 lg:p-20"
      >
        <div
          id="budget-add-layer-presentation-wrapper"
          className="text-center p-5 md:p-10 lg:p-10"
        >
          <div className="text-2xl text-center font-semibold text-white pt-4 md:text-3xl lg:text-4xl">
            Hemen Başlayın!
          </div>
          <div className="text-base text-center text-white p-8">
            Kendi bütçe yönetim panelinizi oluşturarak haftalık, aylık hatta
            yıllık olarak düzenli bir biçimde gelir ve giderlerinizi takip
            etmeye başlayabilirsiniz.
          </div>
          <div
            id="add-budget-btn"
            className="flex items-center justify-center pt-2 pb-2 rounded-2xl mx-auto bg-[#f3796e] hover:bg-white md:w-64 lg:w-64"
            onMouseOver={() => handleHover(true)}
            onMouseLeave={() => handleHover(false)}
            style={{  cursor: "pointer" }}
          >
            <IoIosAddCircle id="add-btn" size={"40"} color="white" />
            <div id="budget-txt" className="text-base text-white ml-3 md:text-xl lg:text-xl">
              Bütçe Ekle
            </div>
          </div>
        </div>
          <img
            src="https://www.workflowmax.com/hubfs/budget%20900x400v2.png"
            className="mt-4 hidden md:block md:my-auto lg:block lg:my-auto"
          />
      </div>
    </>
  );
};

export default Home;
