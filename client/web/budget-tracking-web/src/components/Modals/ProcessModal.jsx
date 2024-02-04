import React, { useEffect, useState } from "react";
import Modal from "react-modal";
import ReactModal from "react-modal";
import { IoMdCloseCircle } from "react-icons/io";
import { useForm } from "react-hook-form";
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
    formState: { errors },
  } = useForm({
    defaultValues: {
      email: "",
      password: "",
    },
  });

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
          <div className="flex">
            <div>{props.type === "add" ? "Gelir Ekleme" : "Gider Ekleme"}</div>
            <button onClick={() => handleModalClose()}>
              <IoMdCloseCircle size={"25"} className="ml-auto" />
            </button>
          </div>
          <form>
            <div className="flex">
              <select>
                {data.categories.map((category) => (
                  <>
                    <option value={category.id}>{category.title}</option>
                  </>
                ))}
              </select>

            </div>
          </form>
        </Modal>
      )}
    </>
  );
};

export default ProcessModal;
