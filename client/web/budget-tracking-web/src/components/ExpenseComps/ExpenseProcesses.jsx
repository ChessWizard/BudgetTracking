/* eslint-disable jsx-a11y/alt-text */
import React from "react";
import { CiMenuKebab } from "react-icons/ci";

const ExpenseProcesses = (props) => {

  return (
    <>
      <div
        id="expense-processes-container"
        className="overflow-y-auto w-full bg-white border border-solid border-1 border-slate-500 rounded p-3
      md:w-1/2 lg:w-2/3"
      style={{scrollbarWidth:"thin"}}
      >
        <div id="process-counts-wrapper" className="flex items-center">
          <div id="count" className="flex items-center md:ml-5 lg:ml-5">
            <div className="text-base mr-3">İşlemler:</div>
            <span className="text-base font-semibold">{props.data.count}</span>
          </div>
          <div
            id="total-price"
            className="flex items-center ml-5 md:ml-auto lg:ml-auto md:mr-5 lg:mr-5"
          >
            <div className="text-base mr-3">Toplam:</div>
            <span style={props.data.totalPrice > 0 ? {color:"#4CAF50"} : {color: "#F44336"}} className="text-base font-semibold">
              {props.data.totalPrice} ₺
            </span>
          </div>
        </div>
        <div id="process-contents-wrapper" className="pt-7 overflow-y-auto">
          {props.data.processes.map((item) => (
            <>
              <div className="flex justify-around mb-4">
                <img src={item.imageUrl} />
                <div>{item.category}</div>
                <div >
                  <span style={item.price > 0 ? {color:"#4CAF50"} : {color: "#F44336"}} className="font-semibold">{item.price} ₺</span>
                  <br />
                  {item.createdDate}
                </div>
                <div
                  id="options-container"
                  style={{ cursor: "pointer" }}
                  className="rounded-full p-2 hover:bg-[#F2F2F2]"
                >
                  <CiMenuKebab size={"30"} />
                </div>

                <div
                  id="expense-type-stick"
                  style={
                    item.expenseType === "Revenue"
                      ? { backgroundColor: "#4CAF50" }
                      : { backgroundColor: "#F44336" }
                  }
                  className="px-1 rounded"
                ></div>
              </div>
            </>
          ))}
        </div>
      </div>
    </>
  );
};

export default ExpenseProcesses;
