/* eslint-disable jsx-a11y/alt-text */
import React from "react";
import Modal from "react-modal";
import ReactModal from "react-modal";
import useWindowSize from "../../hooks/CheckWindowHeight";
import { IoMdCloseCircle } from "react-icons/io";
import { getCategoryIcons } from "../../helpers/IconHelper";

const PhotoModal = (props) => {
  const [width, height] = useWindowSize();

  ReactModal.defaultStyles.overlay.backgroundColor = "#3D3D3D";
  ReactModal.defaultStyles.overlay.opacity = "1";
  ReactModal.defaultStyles.overlay.zIndex = "2000"; // header'ı da kapatabilmesi için
  ReactModal.defaultStyles.overlay.position = "absolute"

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

  const handleModalClose = () => {
    props.handlePhotoModalClose(false);
  };

  const handleSelectedIcon = (icon) => {
    props.handleClickedImage(icon)
    var photoModal = document.getElementById("photo-modal")
    console.log("Photo Model: " + photoModal.isOpen)
    // varlığı dışarıda olduğu için kapanması gerektiğini dışarıya bildirmeliyiz!
    // bu sayede üst komponentten kapatılmış olur
    props.handlePhotoModalClose(false);
  }

  return (
    <div>
      <Modal
        id="photo-modal"
        isOpen={props.isOpen}
        contentLabel="Photo Modal"
        style={customStyles}
      >
        <div className="flex justify-between">
          <div className="mr-7 text-xl font-semibold">İKONLAR</div>
          <button onClick={() => handleModalClose()}>
            <IoMdCloseCircle size={"25"} className="ml-auto" />
          </button>
        </div>

        <div className="grid grid-cols-5 mt-5">
            {getCategoryIcons().map(icon => (
                <img src={icon} className="rounded-full border shadow-lg" style={{cursor:"pointer"}}
                onClick={() => handleSelectedIcon(icon)} />
            ))}
        </div>
      </Modal>
    </div>
  );
};

export default PhotoModal;
