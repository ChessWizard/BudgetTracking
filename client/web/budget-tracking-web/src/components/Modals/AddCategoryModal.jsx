import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { IoMdCloseCircle } from "react-icons/io";
import Modal from "react-modal";
import ReactModal from "react-modal";
import useWindowSize from "../../hooks/CheckWindowHeight";
import PhotoModal from "./PhotoModal";
import axios from "axios";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../../features/auth/authSlice";

const AddCategoryModal = (props) => {
  const [width, height] = useWindowSize();
  const [openPhotoModel, setOpenPhotoModel] = useState(false);

  ReactModal.defaultStyles.overlay.backgroundColor = "#3D3D3D";
  ReactModal.defaultStyles.overlay.opacity = "1";
  ReactModal.defaultStyles.overlay.zIndex = "2000"; // header'ı da kapatabilmesi için

  const customStyles = {
    content: {
      top: "50%",
      left: "50%",
      right: "auto",
      bottom: "auto",
      padding: "2rem",
      transform: "translate(-50%, -50%)",
      opacity: "2 !important",
      width: "100%",
      // MD ve LG ekranlar için özel stil
      ...(width > 768 && {
        // MD ekranlar için
        maxHeight: "90%",
        width: "50%",
      }),
      ...(width > 1024 && {
        // LG ekranlar için
        maxHeight: "90%",
        width: "35%",
      }),
    },
  };

  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm({
    defaultValues: {
      expenseType: "",
      title: "",
      imageUrl: "",
    },
  });

  const handleModalClose = () => {
    props.handleModalClose(false);
  };

  const handlePhotoModalClose = (value) => {
    setOpenPhotoModel(value);
  };

  const handleOpenPhotoModel = () => {
    setOpenPhotoModel(true);
    console.log(openPhotoModel);
  };

  const handleClickedImage = (value) => {
    setValue("imageUrl", value);
    var categoryIcon = document.getElementById("category-icon");
    categoryIcon.src = value;
    categoryIcon.style.padding = "0";
  };

  const accessToken = useSelector(selectCurrentToken);
  const createCategoryByUser = (data) => {

    // expenseType'ın props üzerinden atanması
    data.expenseType = props.categoryType

    axios
      .post("https://budgettracking77.azurewebsites.net/api/Categories", data, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then((response) => {
        console.log(response);
        props.handleHasProcessed(true)
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <>
      <Modal
        isOpen={props.isOpen}
        contentLabel="Add Category Modal"
        style={customStyles}
      >
        {/* İlk açılacak modal'ın içine koyduk ki çağrılınca onun da üstünde kalsın photo modal */}
        <PhotoModal
          isOpen={openPhotoModel}
          handlePhotoModalClose={handlePhotoModalClose}
          handleClickedImage={handleClickedImage}
        />
        <div className="flex justify-between">
          <div className="mr-7 text-xl font-semibold">Kategori Ekleme</div>
          <button onClick={() => handleModalClose()}>
            <IoMdCloseCircle size={"25"} className="ml-auto" />
          </button>
        </div>
        <form onSubmit={handleSubmit((data) => createCategoryByUser(data))}>
          <div id="title-container">
            <div className="mt-5 mb-3">İsim</div>
            <input
              type="text"
              className="mb-3 p-3 ml-1 w-full border border-black rounded md:h-1/2 lg:1/2
                md:ml-2 lg:ml-2"
              {...register("title")}
            />
          </div>

          <div id="photo-container" className="flex">
            <div className="mt-3">Resim</div>
            <img
              id="category-icon"
              className="rounded-full border border-black p-5 ml-5"
              onClick={() => handleOpenPhotoModel()}
            />
          </div>
          <button
            type="submit"
            className={`mt-5 w-full p-2 rounded text-white ${
              props.categoryType === "Revenue" ? "bg-[#4CAF50]" : "bg-[#F44336]"
            }`}
          >
            KAYDET
          </button>
        </form>
      </Modal>
    </>
  );
};

export default AddCategoryModal;
