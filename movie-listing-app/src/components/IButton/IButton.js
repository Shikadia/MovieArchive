import React from "react";

const IButton = ({ text, type, onClick }) => {
  return (
    <button
      type={type}
      onClick={onClick}
      style={{
        background: "#000",
        color: "#fff",
        padding: "8px 10px",
        borderRadius: "5px",
        marginRight: "10px",
      }}
    >
      {text}
    </button>
  );
};

export default IButton;
