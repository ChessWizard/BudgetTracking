import React from "react";
import { IoMdAdd } from "react-icons/io";
import { RiSubtractFill } from "react-icons/ri";

const ProcessButton = (props) => {
  
  const getIconByType = (type) => {
    switch (type) {
      case "add":
        return <IoMdAdd size={"25"} color="white" />;
      case "subtract":
        return <RiSubtractFill size={"25"} color="white" />;
      default:
        break;
    }
  };

  return (
    <>
      <div
        style={{cursor:"pointer"}}
        className={`${props.padding} ${props.margin} rounded-full ${props.backgroundColor}
        flex justify-center items-center opacity-90 hover:opacity-100`}
      >
        {getIconByType(props.type)}
      </div>
    </>
  );
};

export default ProcessButton;
