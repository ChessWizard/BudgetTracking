import React, { useEffect, useState } from "react";
import AddPaymentAccountModal from "../components/Modals/AddPaymentAccountModal";
import ProcessButton from "../components/Buttons/ProcessButton";
import axios from "axios";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../features/auth/authSlice";
import { CiMenuKebab } from "react-icons/ci";
import { IoWalletOutline } from "react-icons/io5";
import { AiOutlineBank, AiOutlineCreditCard } from "react-icons/ai";

const PaymentAccount = () => {
  const [selectedElement, setSelectedElement] = useState("Wallet");
  const [data, setData] = useState(null);
  const [hasError, setError] = useState(false);
  const [loading, setLoading] = useState(true);
  const [isModalOpen, setModalOpen] = useState(false)
  const [hasProcessed, setHasProcessed] = useState(false)

  const accessToken = useSelector(selectCurrentToken);
  const getPaymentAccountByType = () => {
    axios
      .get(
        `https://budgettracking77.azurewebsites.net/api/Payments?paymentType=${selectedElement}`,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      )
      .then((response) => {
        setError(false);
        setData(response.data.data);
      })
      .catch((error) => {
        setError(true);
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
    getPaymentAccountByType();
  }, [selectedElement, hasProcessed]);

  return (
    <>
      <div
        id="payment-container"
        className=" w-full flex flex-col justify-center items-center"
        style={{ backgroundColor: "#F2F2F2", maxHeight: "100vh", scrollbarWidth:"thin" }}
      >
        <AddPaymentAccountModal
          isOpen={isModalOpen}
          handleModalClose={(value) => handleModalClose(value)}
          paymentType={selectedElement}
          handleHasProcessed = {(value) => setHasProcessed(value)}
        />
        <div
          id="payment-content-container"
          className="overflow-y-auto w-full md:w-1/2 lg:w-2/3"
        >
          <div
            id="tabbar-elements"
            className="grid grid-cols-3 border border-slate-300 shadow w-full"
            style={{ backgroundColor: "#F2F2F2" }}
          >
            <div
              onClick={() => setSelectedElement("Wallet")}
              className={`text-sm text-center  ml-2 mt-4 mr-3 p-3 rounded-tl-md rounded-tr-md
              md:text-base lg:text-base 
            ${selectedElement === "Wallet" && "text-white bg-[#0096FF]"}`}
              style={{ cursor: "pointer" }}
            >
              Cüzdan
            </div>
            <div
              onClick={() => setSelectedElement("Bank")}
              className={`text-sm text-center  ml-2 mt-4 mr-3 p-3 rounded-tl-md rounded-tr-md
              md:text-base lg:text-base
            ${selectedElement === "Bank" && "text-white bg-[#0096FF]"}`}
              style={{ cursor: "pointer" }}
            >
              Banka
            </div>
            <div
              onClick={() => setSelectedElement("Credit")}
              className={`text-sm text-center  ml-2 mt-4 mr-3 p-3 rounded-tl-md rounded-tr-md
              md:text-base lg:text-base
            ${selectedElement === "Credit" && "text-white bg-[#0096FF]"}`}
              style={{ cursor: "pointer" }}
            >
              Kredi Kartı
            </div>
          </div>
          <div
            id="payments-by-type-container"
            className="p-4 bg-[white] md:p-3 lg:p-4"
          >
            {hasError && <div>Hesap bulunamadı...</div>}
            {!loading &&
              !hasError &&
              data.payments.map((payment) => (
                <>
                  <div
                    className="flex justify-start items-center mb-5"
                    style={{ cursor: "pointer"}}
                  >
                    {selectedElement === "Wallet" ? (
                      <IoWalletOutline size={"25"} />
                    ) : selectedElement === "Bank" ? (
                      <AiOutlineBank size={"25"} />
                    ) : (
                      <AiOutlineCreditCard size={"25"} />
                    )}
                    <p className="ml-4">{payment.title}</p>
                    <span className="ml-auto" style={{ cursor: "pointer" }}>
                      <div className="flex">
                        <p className={`mb-3 font-semibold ${parseFloat(payment.amount) > 0 ? "text-[#4CAF50]" : "text-[#F44336]"}`}>{payment.amount}</p>
                        <span className={`ml-1 font-semibold ${parseFloat(payment.amount) > 0 ? "text-[#4CAF50]" : "text-[#F44336]"}`}>
                        {payment.currencyCode === "TRY" ? "₺" :
                        payment.currencyCode === "USD" ? "$" :
                        payment.currencyCode === "EUR" ? "€" : ""}
                        </span>
                      </div>
                      <CiMenuKebab size={"30"} />
                    </span>
                  </div>
                </>
              ))}
          </div>
        </div>
        <div
          className="absolute bottom-10 right-10"
            onClick={() => setModalOpen(true)}
        >
          <ProcessButton
            padding={"p-5"}
            margin={"mb-3"}
            backgroundColor={"bg-[#0096FF]"}
            type={"add"}
          />
        </div>
      </div>
    </>
  );
};

export default PaymentAccount;
