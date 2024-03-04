import { useEffect, useState } from "react";
import WebFont from "webfontloader";
import logo from "../logo.svg";

const Home = () => {
  useEffect(() => {
    WebFont.load({
      google: {
        families: ["Karla", "Poppins"],
      },
    });
  }, []);

  const styles = {
    backgroundImage: `url(${logo}})`,
    backgroundSize: "cover",
    backgroundRepeat: "no-repeat",
    backgroundPosition: "center",
  };

  return (
    <div className="home-container">
      <div className="first-home-section">
        <div className="first-content-container">
          <div className="first-section-scond-container">
            <img className="second-container-image" src={logo} alt="image" />
            <p className="first-section-paragraph">
              Outstanding in Learning and Character
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Home;
