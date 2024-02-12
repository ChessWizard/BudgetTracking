import React, { useState } from "react";
import ProcessButton from "../components/Buttons/ProcessButton";
import ExpenseProcesses from "../components/ExpenseComps/ExpenseProcesses";
import axios from "axios";
import { useEffect } from "react";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../features/auth/authSlice";
import ProcessModal from "../components/Modals/ProcessModal";
import { FaDownload } from "react-icons/fa6";
import { useNavigate } from "react-router-dom";

const Expense = () => {
  const [expenseData, setExpenseData] = useState(null);
  const [isLoading, setLoading] = useState(true);
  const [isModalOpen, setModalOpen] = useState(false);
  const [selectedProcessType, setSelectedProcessType] = useState();
  const [isProcessed, setProcessed] = useState(null);
  const navigate = useNavigate()
  const [month, setMonth] = useState(2);
  const [year, setYear] = useState(2024);

  const accessToken = useSelector(selectCurrentToken);

  const getExpensesByUser = () => {
    axios
      .get(
        `https://budgettracking77.azurewebsites.net/api/Expenses/from/date?month=${month}&year=${year}`,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      )
      .then((response) => {
        console.log(response);
        setExpenseData(response.data.data);
        setTimeout(() => {
          setLoading(false);
        }, 800);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  useEffect(() => {
    getExpensesByUser();
  }, [isProcessed]);

  const handleAddProcessBtn = () => {
    console.log("Opened");
    setSelectedProcessType("add");
    setModalOpen(true);
  };

  const handleSubtractProcessBtn = () => {
    console.log("Opened");
    setSelectedProcessType("subtract");
    setModalOpen(true);
  };

  const handleModalClose = (value) => {
    setModalOpen(value);
  };

  const handleProcessed = (value) => {
    setProcessed(value);
  };

  

  return (
    <>
      {!isLoading && (
        <>
          <div
            id="expense-container"
            style={{ backgroundColor: "#F2F2F2" }}
            className="w-full flex flex-col justify-center items-center"
          >
            {isModalOpen && (
              <ProcessModal
                isOpen={isModalOpen}
                type={selectedProcessType}
                handleModalClose={(value) => handleModalClose(value)}
                handleProcessed={handleProcessed}
              />
            )}
            <div className="flex justify-center items-center">
              <div
                id="navigate-to-report"
                className="flex p-3 bg-[#4CAF50] mb-5 text-white font-semibold text-base rounded md:text-lg lg:text-lg"
                onClick={() => navigate("/dashboard/report/transaction")}
                style={{ cursor: "pointer" }}
              >
                <FaDownload size={"20"} />
                <span className="mx-3">Rapor</span>
              </div>
            </div>
            <ExpenseProcesses data={expenseData} />
            <div id="process-btn-group" className="absolute bottom-10 right-10">
              {/* sağ ve aşağı hizalama */}
              <div
                onClick={() => handleAddProcessBtn()}
                style={{ cursor: "pointer" }}
              >
                <ProcessButton
                  padding={"p-5"}
                  margin={"mb-3"}
                  backgroundColor={"bg-[#4CAF50]"}
                  type={"add"}
                />
              </div>
              <div
                onClick={() => handleSubtractProcessBtn()}
                style={{ cursor: "pointer" }}
              >
                <ProcessButton
                  padding={"p-5"}
                  backgroundColor={"bg-[#F44336]"}
                  type={"subtract"}
                />
              </div>
            </div>
          </div>
        </>
      )}
      {expenseData === null && (
        <>
          <div>Herhangi bir gider bulunamadı</div>
          {isModalOpen && (
            <ProcessModal
              isOpen={isModalOpen}
              type={selectedProcessType}
              handleModalClose={(value) => handleModalClose(value)}
              handleProcessed={handleProcessed}
            />
          )}
          <div id="process-btn-group" className="absolute bottom-10 right-10">
            {/* sağ ve aşağı hizalama */}
            <div
              onClick={() => handleAddProcessBtn()}
              style={{ cursor: "pointer" }}
            >
              <ProcessButton
                padding={"p-5"}
                margin={"mb-3"}
                backgroundColor={"bg-[#4CAF50]"}
                type={"add"}
              />
            </div>
            <div
              onClick={() => handleSubtractProcessBtn()}
              style={{ cursor: "pointer" }}
            >
              <ProcessButton
                padding={"p-5"}
                backgroundColor={"bg-[#F44336]"}
                type={"subtract"}
              />
            </div>
          </div>
        </>
      )}
    </>
  );
};

export default Expense;
