import axios from "axios";
import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../features/auth/authSlice";
import { CiMenuKebab } from "react-icons/ci";
import ProcessButton from "../components/Buttons/ProcessButton";
import AddCategoryModal from "../components/Modals/AddCategoryModal";

const Category = () => {
  const [selectedElement, setSelectedElement] = useState("Revenue");
  const [data, setData] = useState(null);
  const [hasCategoryError, setCategoryError] = useState(false);
  const [loading, setLoading] = useState(true);
  const [isModalOpen, setModalOpen] = useState(false);
  const [hasProcessed, setHasProcessed] = useState(false)

  const accessToken = useSelector(selectCurrentToken);

  const getCategoriesByExpense = () => {
    axios
      .get(
        `https://budgettracking77.azurewebsites.net/api/Categories?expenseType=${selectedElement}`,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      )
      .then((response) => {
        setData(response.data.data);
      })
      .catch((error) => {
        setCategoryError(true);
        console.log(error);
      })
      .finally(() => {
        setLoading(false);
      });
  };

  const handleModalClose = (value) => {
    setModalOpen(value)
  }

  useEffect(() => {
    getCategoriesByExpense();
    console.log(selectedElement)
  }, [selectedElement, hasProcessed]);

  return (
    <>
      <div
        id="category-container"
        className="w-full flex flex-col justify-center items-center"
        style={{ backgroundColor: "#F2F2F2", maxHeight: "100vh" }}
      >
      <AddCategoryModal
                isOpen={isModalOpen}
                handleModalClose={(value) => handleModalClose(value)}
                categoryType = {selectedElement}
                handleHasProcessed = {(value) => setHasProcessed(value)}
              />
        <div
          id="category-content-container"
          className="w-full md:w-1/2 lg:w-2/3"
        >
          <div
            id="tabbar-elements"
            className="grid grid-cols-2 border border-slate-300 shadow w-full"
            style={{ backgroundColor: "#F2F2F2" }}
          >
            <div
              onClick={() => setSelectedElement("Revenue")}
              className={`ml-2 mt-4 mr-3 p-3 rounded-tl-md rounded-tr-md text-center 
            ${selectedElement === "Revenue" && "text-white bg-[#4CAF50]"}`}
              style={{ cursor: "pointer" }}
            >
              Gelirler
            </div>
            <div
              onClick={() => setSelectedElement("Outgoing")}
              className={`ml-2 mt-4 mr-3 p-3 rounded-tl-md rounded-tr-md text-center 
            ${selectedElement === "Outgoing" && "text-white bg-[#F44336]"}`}
              style={{ cursor: "pointer" }}
            >
              Giderler
            </div>
          </div>
          <div
            id="categories-by-expense-container"
            className="p-2 bg-[white] md:p-3 lg:p-4"
          >
            {!loading &&
              !hasCategoryError &&
              data.categories.map((category) => (
                <>
                  <div
                    className="flex justify-start items-center mb-3"
                    style={{ cursor: "pointer" }}
                  >
                    <img src={category.imageUrl} />
                    <p className="ml-4">{category.title}</p>
                    <span className="ml-auto" style={{ cursor: "pointer" }}>
                      <CiMenuKebab size={"30"} />
                    </span>
                  </div>
                </>
              ))}
          </div>
        </div>
        <div className="absolute bottom-10 right-10" onClick={() => setModalOpen(true)}>
          <ProcessButton
            padding={"p-5"}
            margin={"mb-3"}
            backgroundColor={`${
              selectedElement === "Revenue" ? "bg-[#4CAF50]" : "bg-[#F44336]"
            }`}
            type={"add"}
          />
        </div>
      </div>
    </>
  );
};

export default Category;
