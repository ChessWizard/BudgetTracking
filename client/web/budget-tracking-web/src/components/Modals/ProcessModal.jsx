import React, { useEffect, useState } from "react";
import Modal from "react-modal";
import ReactModal from "react-modal";
import { IoMdCloseCircle } from "react-icons/io";
import { useForm, Controller } from "react-hook-form";
import axios from "axios";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../../features/auth/authSlice";
import useWindowSize from "../../hooks/CheckWindowHeight";

const ProcessModal = (props) => {
  const [data, setData] = useState(null);
  const accessToken = useSelector(selectCurrentToken);
  const [userPaymentAccounts, setUserPaymentAccounts] = useState(null);
  const [isCategoryLoading, setCategoryLoading] = useState(true);
  const [hasCategoryError, setCategoryError] = useState(false);
  const [isPaymentLoading, setPaymentLoading] = useState(true);
  const [hasPaymentError, setPaymentError] = useState(false);
  const [width, height] = useWindowSize();

  // Modal content css'i
  const customStyles = {
    content: {
      top: "50%",
      left: "50%",
      right: "auto",
      bottom: "auto",
      padding: "2rem",
      transform: "translate(-50%, -50%)",
      opacity: "2 !important",
      maxHeight: "100%", // modal içeriği %90 lık bir alan kaplasın
      width: "100%",
      // MD ve LG ekranlar için özel stil
      ...(width > 768 && {
        // MD ekranlar için
        maxHeight: "90%",
        width: "75%",
      }),
      ...(width > 1024 && {
        // LG ekranlar için
        maxHeight: "90%",
        width: "50%",
      }),
    },
  };

  // ReactModal yapısının tamamı için stiller(base styles)
  ReactModal.defaultStyles.overlay.backgroundColor = "#3D3D3D";
  ReactModal.defaultStyles.overlay.opacity = "1";
  ReactModal.defaultStyles.overlay.zIndex = "2000"; // header'ı da kapatabilmesi için
  ReactModal.defaultStyles.overlay.overflowY = "auto";
  ReactModal.defaultStyles.overlay.maxHeight = "100%"; // tüm modal sayfanın %100'ünü kaplasın

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
      paymentAccountId: "",
      price: 0,
      expenseType: props.type === "add" ? 0 : 1,
      description: "",
      currencyCode: "",
      processDate: "",
      processTime: "",
    },
  });

  const handleCategoryChange = (selectedCategoryId) => {
    setValue("categoryId", selectedCategoryId);
  };

  const createExpenseByUser = (data) => {
    // TimeOnly tipinden dolayı saniye eklemeliyiz
    data.processTime = data.processTime + ":00";

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
      })
      .catch((error) => {
        console.log(error);
        setCategoryError(true);
      })
      .finally(() => {
        setCategoryLoading(false);
      });
  };

  const getUserPaymentAccounts = () => {
    axios
      .get("https://budgettracking77.azurewebsites.net/api/Payments", {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      })
      .then((response) => {
        setUserPaymentAccounts(response.data.data);
      })
      .catch((error) => {
        console.log(error);
        setPaymentError(true);
      })
      .finally(() => {
        setPaymentLoading(false);
      });
  };

  useEffect(() => {
    getCategoriesByUser();
    getUserPaymentAccounts();
  }, []);

  return (
    <>
      {!isCategoryLoading && !isPaymentLoading && (
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
                        {hasCategoryError ? (
                          <option>-</option>
                        ) : (
                          data.categories.map((category) => (
                            <>
                              <option value={category.id}>
                                {category.title}
                              </option>
                            </>
                          ))
                        )}
                      </select>
                    </>
                  )}
                />
                <div className="p-3">Hesap</div>
                <Controller
                  name="paymentAccountId"
                  control={control}
                  render={({ field }) => (
                    <>
                      <select
                        {...field}
                        onChange={(e) => handleCategoryChange(e.target.value)}
                        className="mb-3 p-2 ml-1 w-full border border-black rounded md:h-1/2 lg:1/2 md:mb-0 lg:mb-0
                md:ml-2 lg:ml-2"
                        {...register("paymentAccountId")}
                      >
                        {hasPaymentError ? (
                          <option>-</option>
                        ) : (
                          userPaymentAccounts.payments.map((payment) => (
                            <>
                              <option value={payment.id}>
                                {payment.title}
                              </option>
                            </>
                          ))
                        )}
                      </select>
                    </>
                  )}
                />
                <div className="p-3">Tarih</div>
                <input
                  type="date"
                  {...register("processDate")}
                  className="mb-3 p-2 ml-1 w-full border border-black rounded md:h-1/2 lg:1/2 md:mb-0 lg:mb-0
                md:ml-2 lg:ml-2"
                />
                <div className="p-3">Saat</div>
                <input
                  type="time"
                  {...register("processTime")}
                  className="mb-3 p-2 ml-1 w-full border border-black rounded md:h-1/2 lg:1/2 md:mb-0 lg:mb-0
                md:ml-2 lg:ml-2"
                />
              </div>
              <div className="p-3">Fiyat</div>
              <div className="grid grid-cols-7">
                <input
                  id="price"
                  type="number"
                  className="border border-black rounded p-1.5 pl-2 ml-2 mb-3 col-span-5"
                  {...register("price")}
                />
                <Controller
                  name="currencyCode"
                  control={control}
                  render={({ field }) => (
                    <>
                      <select
                        {...field}
                        onChange={(e) => handleCategoryChange(e.target.value)}
                        className="col-span-2 border border-black rounded p-1.5 pl-2 ml-2 mb-3"
                        {...register("currencyCode")}
                      >
                        <option value="" disabled selected>
                          Birim
                        </option>
                        <option value="TRY">₺</option>
                        <option value="USD">$</option>
                        <option value="EUR">€</option>
                      </select>
                    </>
                  )}
                />
              </div>
              <div>
                <div className="p-3">Açıklama</div>
                {/* Çok satırlı text alanı */}
                <textarea
                  className="border border-black rounded w-full p-1.5 pl-2 ml-2 h-32 resize-none"
                  {...register("description")}
                ></textarea>
              </div>
              <button
                type="submit"
                className={`mt-5 ml-2 p-1 text-white rounded ${
                  props.type === "add" ? "bg-[#4CAF50]" : "bg-[#F44336]"
                } `}
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
