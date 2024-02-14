import React, { useState } from 'react'
import useWindowSize from '../../hooks/CheckWindowHeight';
import ReactModal from 'react-modal';
import Modal from "react-modal";
import { Controller, useForm } from 'react-hook-form';
import { useSelector } from 'react-redux';
import { selectCurrentToken } from '../../features/auth/authSlice';
import axios from 'axios';
import { IoMdCloseCircle } from 'react-icons/io';

const AddPaymentAccountModal = (props) => {
    const [width, height] = useWindowSize();
    const [openPhotoModel, setOpenPhotoModel] = useState(false);
    const [checkProcessed, setProcessed] = useState(false)
  
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
      control,
      handleSubmit,
      setValue,
      formState: { errors },
    } = useForm({
      defaultValues: {
        title: "",
        initialAmount: "",
        currencyCode: "",
        paymentType: "",
        description: ""
      },
    });
  
    const handleModalClose = () => {
      props.handleModalClose(false);
    };

    const handleCurrencyChange = (selectedCurrency) => {
        setValue("currencyCode", selectedCurrency)
    }

    const accessToken = useSelector(selectCurrentToken);
    const createPaymentAccountByUser = (data) => {
      
      data.paymentType = props.paymentType
        
      axios
        .post("https://budgettracking77.azurewebsites.net/api/Payments", data, {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        })
        .then((response) => {
          console.log(response);
          setProcessed(!checkProcessed)
          props.handleHasProcessed(!checkProcessed)
        })
        .catch((error) => {
          console.log(error);
        });
    };
  
    return (
      <>
        <Modal
          isOpen={props.isOpen}
          contentLabel="Add Payment Account Modal"
          style={customStyles}
        >
          <div className="flex justify-between">
            <div className="mr-7 text-xl font-semibold">
              Hesap Ekleme
            </div>
            <button onClick={() => handleModalClose()}>
              <IoMdCloseCircle size={"25"} className="ml-auto" />
            </button>
          </div>
          <form id="create-payment-form" className='mt-5' onSubmit={handleSubmit(data => createPaymentAccountByUser(data)) }>
            <p className='p-3 mb-2'>İsim</p>
            <input type='text' className="mb-4 p-2 ml-1 w-full border border-black rounded md:h-1/2 lg:1/2 md:mb-0 lg:mb-0
                md:ml-2 lg:ml-2"
                    {...register("title")}
                />
            <p className='p-3 mb-2'>Fiyat</p>    
            <div className="grid grid-cols-7">
                <input
                  id="price"
                  type="number"
                  className="border border-black rounded p-2 pl-2 ml-2 mb-4 col-span-5"
                  {...register("initialAmount")}
                />
                <Controller
                  name="currencyCode"
                  control={control}
                  render={({ field }) => (
                    <>
                      <select
                        {...field}
                        onChange={(e) => handleCurrencyChange(e.target.value)}
                        className="col-span-2 border border-black rounded pl-2 ml-2 mb-3"
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
              <div className="p-3 mb-2">Açıklama</div>
                {/* Çok satırlı text alanı */}
                <textarea
                  className="border border-black rounded w-full p-1.5 pl-2 ml-2 h-32 resize-none"
                  {...register("description")}
                ></textarea>
                <button
                type="submit"
                className={`w-full p-2 mt-5 ml-2 text-white rounded bg-[#0096FF]`}
              >
                Ekle
              </button>
          </form>
        </Modal>
      </>
    );
}

export default AddPaymentAccountModal
