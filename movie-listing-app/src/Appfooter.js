import { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";

const Appfooter = () => {
  const location = useLocation();
  const [showmenu, setShowmenu] = useState(false);

  useEffect(() => {
    if (
      location.pathname === "/" ||
      location.pathname === "/movies" ||
      location.pathname === "/actors"
    ) {
      setShowmenu(true);
    } else {
      setShowmenu(false);
    }
  }, []);
  return (
    <div>
      {showmenu && (
        <footer>
          {/* <p className="footer">Movies &copy; 2024 my App</p> */}
        </footer>
      )}
    </div>
  );
};

export default Appfooter;
