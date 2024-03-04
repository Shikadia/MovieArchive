import React from "react";

const IInput = ({ type, placeholder, value, onChange }) => {
  return (
    <input
      type={type}
      placeholder={placeholder}
      value={value}
      onChange={onChange}
      style={{
        padding: "8px",
        borderRadius: "6px",
        border: "1px solid gray",
        outline: "0px",
      }}
    />
  );
};

export default IInput;
