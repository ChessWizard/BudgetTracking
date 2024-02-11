import React, { useEffect, useState } from "react";
import Modal from "react-modal";
import ReactModal from "react-modal";
import { IoMdCloseCircle } from "react-icons/io";
import { useForm, Controller } from "react-hook-form";
import axios from "axios";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../../features/auth/authSlice";

const ProcessModal = (props) => {
  const [data, setData] = useState(null);
  const accessToken = useSelector(selectCurrentToken);
  const [isLoading, setLoading] = useState(true);

  const customStyles = {
    content: {
      top: "50%",
      left: "50%",
      right: "auto",
      bottom: "auto",
      padding: "2rem",
      transform: "translate(-50%, -50%)",
      opacity: "2 !important",
    },
  };

  // ReactModal'ın arka planı için styles
  ReactModal.defaultStyles.overlay.backgroundColor = "#3D3D3D";
  ReactModal.defaultStyles.overlay.opacity = "0.94";
  ReactModal.defaultStyles.overlay.zIndex = "2000"; // header'ı da kapatabilmesi için

  const handleModalClose = () => {
    console.log("Closed");
    props.handleModalClose(false);
  };

  const {
    register,
    handleSubmit,
    control,
    setValue,
    formState: { errors },
  } = useForm({
    defaultValues: {
      categoryId: "",
      price: 0,
      expenseType: props.type === "add" ? 0 : 1,
      description: "",
    },
  });

  const handleCategoryChange = (selectedCategoryId) => {
    setValue("categoryId", selectedCategoryId);
  };

  const createExpenseByUser = (data) => {
    axios
      .post("https://budgettracking77.azurewebsites.net/api/Expenses", data, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then((response) => {
        console.log(response);
        // TODO: Üst komponenti tetikleme akışı değerlendirilmeli
        props.handleProcessed(response);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const getCategoriesByUser = () => {
    axios
      .get(
        `https://budgettracking77.azurewebsites.net/api/Categories?expenseType=${
          props.type === "add" ? 0 : 1
        }`,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      )
      .then((response) => {
        console.log(response.data.data);
        setData(response.data.data);
        setTimeout(() => {
          setLoading(false);
        }, 200);
      })
      .catch((error) => {
        console.log(error);
        setLoading(false);
      });
  };

  useEffect(() => {
    getCategoriesByUser();
  }, []);

  return (
    <>
      {!isLoading && (
        <Modal
          isOpen={props.isOpen}
          style={customStyles}
          contentLabel="Processing Modal"
        >
          <div className="flex justify-between">
            <div className="mr-7 text-xl font-semibold">
              {props.type === "add" ? "Gelir Ekleme" : "Gider Ekleme"}
            </div>
            <button onClick={() => handleModalClose()}>
              <IoMdCloseCircle size={"25"} className="ml-auto" />
            </button>
          </div>
          <form onSubmit={handleSubmit((data) => createExpenseByUser(data))}>
            <div className="grid grid-cols-none md:p-5 lg:p-5">
              <div className="mt-auto">
                <div className="p-3">Kategori</div>
                <Controller
                  name="categoryId"
                  control={control}
                  render={({ field }) => (
                    <>
                      <select
                        {...field}
                        onChange={(e) => handleCategoryChange(e.target.value)}
                        className="mb-3 p-2 ml-1 w-full border border-black rounded md:h-1/2 lg:1/2 md:mb-0 lg:mb-0
                md:ml-2 lg:ml-2"
                        {...register("categoryId")}
                      >
                        {data.categories.map((category) => (
                          <>
                            <option value={category.id}>
                              {category.title}
                            </option>
                          </>
                        ))}
                      </select>
                    </>
                  )}
                />
              </div>
              <div>
                <div className="p-3">Değer</div>
                <input
                  id="price"
                  type="number"
                  className="border border-black rounded p-1.5 pl-2 ml-2 mb-3"
                  {...register("price")}
                />
              </div>
              <div>
                <div className="p-3">Açıklama</div>
                <input
                  type="text"
                  className="border border-black rounded p-1.5 pl-2 ml-2 h-32"
                  {...register("description")}
                />
              </div>
              <button
                type="submit"
                className={`mt-5 ml-2 p-1 text-white rounded ${props.type === "add" ? "bg-[#4CAF50]" : "bg-[#F44336]"} `}
              >
                Ekle
              </button>
            </div>
          </form>
        </Modal>
      )}
    </>
  );
};

export default ProcessModal;
