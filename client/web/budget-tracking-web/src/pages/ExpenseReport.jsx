import axios from "axios";
import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../features/auth/authSlice";
import { Controller, useForm } from "react-hook-form";

const ExpenseReport = () => {
  const accessToken = useSelector(selectCurrentToken);
  const [selectedExportFileType, setSelectedExportFileType] = useState("Excel");
  const [userPaymentAccounts, setUserPaymentAccounts] = useState(null);
  const [loading, setLoading] = useState(true);

  const {
    register,
    handleSubmit,
    control,
    setValue,
    formState: { errors },
  } = useForm({
    defaultValues: {
      processStartDate: null,
      processEndDate: null,
      paymentAccountId: null,
      exportFileType: "Excel",
    },
  });

  // Dosyayı indirmek için bir fonksiyon
  const downloadFile = (data, fileName) => {
    if (navigator.msSaveBlob) {
      // IE 10+
      navigator.msSaveBlob(data, fileName);
    } else {
      const link = document.createElement("a");
      link.href = window.URL.createObjectURL(data);
      link.setAttribute("download", fileName);
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }
  };

  const handleExportFileTypeChange = (exportFileType) => {
    setValue("exportFileType", exportFileType);
    setSelectedExportFileType(exportFileType);
  };

  const handlePaymentAccountChange = (paymentAccountId) => {
    setValue("paymentAccountId", paymentAccountId);
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
      })
      .finally(() => {
        setLoading(false);
      });
  };

  useEffect(() => {
    getUserPaymentAccounts();
  }, []);

  const handleExportAsExcel = (data) => {
    // Boş stringleri null'a dönüştür, form'dan boş olunca null yerine "" geliyordu
    const transformedData = Object.entries(data).reduce((acc, [key, value]) => {
      acc[key] = value === "" ? null : value;
      return acc;
    }, {});
    axios
      .post(
        "https://budgettracking77.azurewebsites.net/api/Files/export/transaction",
        // from body
        transformedData,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
          responseType: "blob",
        }
      )
      .then((response) => {
        console.log(response);
        downloadFile(response.data, "report");
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleExport = (data) => {
    switch (selectedExportFileType) {
      case "Excel":
        handleExportAsExcel(data);
        break;
      case "PDF":
        break;

      default:
        break;
    }
  };

  return (
    <>
      {!loading && (
        <div
          id="expense-report-container"
          style={{ backgroundColor: "#F2F2F2" }}
          className="w-full flex flex-col justify-center items-center"
        >
          <div className="bg-white p-5 rounded shadow w-full md:w-1/2 lg:w-2/3">
            <p className="text-lg text-center font-semibold">RAPOR OLUŞTUR</p>
            <form
              id="expense-report-form"
              className="mt-3 w-full"
              onSubmit={handleSubmit((data) => handleExport(data))}
            >
              <div
                id="date-container"
                className="grid grid-cols-none md:grid-cols-2 lg:grid-cols-2"
              >
                <div id="start-date" className="mb-3 md:mr-3 lg-mr-3">
                  <p className="mb-2">Şu Tarihten:</p>
                  <input
                    type={"date"}
                    className="border border-black w-full rounded p-1"
                    {...register("processStartDate")}
                  />
                </div>
                <div id="end-date">
                  <p className="mb-2">Şu Tarihe:</p>
                  <input
                    type={"date"}
                    className="border border-black w-full rounded p-1"
                    {...register("processEndDate")}
                  />
                </div>
              </div>
              <div
                id="selection-container"
                className="grid grid-cols-none mt-3 md:grid-cols-2 lg:grid-cols-2"
              >
                <div
                  id="payment-selection"
                  className="mb-3 md:mr-3 lg-mr-3 md:mb-0 lg:mb-0"
                >
                  <Controller
                    name="paymentAccountId"
                    control={control}
                    render={({ field }) => (
                      <>
                        <p className="mb-2">Hesap:</p>
                        <select
                          {...field}
                          onChange={(e) => handlePaymentAccountChange(e.target.value)}
                          className="p-2 w-full border border-black rounded md:h-1/2 lg:1/2 md:mb-0 lg:mb-0"
                          {...register("paymentAccountId")}
                        >
                          {userPaymentAccounts.payments.map((payment) => (
                            <>
                              <option value={payment.id}>
                                {payment.title}
                              </option>
                            </>
                          ))}
                        </select>
                      </>
                    )}
                  />
                </div>
                <div id="file-type-selection">
                  <Controller
                    name="exportFileType"
                    control={control}
                    render={({ field }) => (
                      <>
                        <p className="mb-2">Rapor Tipi:</p>
                        <select
                          {...field}
                          onChange={(e) =>
                            handleExportFileTypeChange(e.target.value)
                          }
                          className="p-2 w-full border border-black rounded md:h-1/2 lg:1/2 md:mb-0 lg:mb-0"
                          {...register("exportFileType")}
                        >
                          <option value="Excel">Excel</option>
                          <option value="PDF">PDF</option>
                        </select>
                      </>
                    )}
                  />
                </div>
              </div>
              <button className="text-white font-semibold p-2 border border rounded bg-[#4CAF50] mt-4 w-full">
                OLUŞTUR
              </button>
            </form>
          </div>
        </div>
      )}
    </>
  );
};

export default ExpenseReport;
