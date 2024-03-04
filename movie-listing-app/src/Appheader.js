import { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";
import IInput from "./components/IInput/IInput";
import IButton from "./components/IButton/IButton";

const Appheader = ({ onSearch }) => {
  const [movieQuery, setMovieQuery] = useState("");
  const [actorQuery, setActorQuery] = useState("");

  const handleInputChange = (event) => {
    setMovieQuery(event.target.value);
  };

  const location = useLocation();
  const [showmenu, setShowmenu] = useState(false);
  useEffect(() => {
    if (
      location.pathname !== "/add_actors" ||
      location.pathname !== "/add_movies" ||
      location.pathname !== "/actors/edit/:id"
    ) {
      setShowmenu(true);
    } else {
      setShowmenu(false);
    }
  }, []);

  return (
    <div>
      {/* {showmenu && ( */}
      <div className="header-container">
        <div className="menu">
          <Link to={"/"} className="menu-link">
            Home
          </Link>
          <Link to={"/movies"} className="menu-link">
            Movies
          </Link>
          <Link to={"/actors"} className="menu-link">
            {" "}
            Actors
          </Link>
        </div>
        <div className="menu1">
          <div
            style={{
              display: "flex",
              alignItems: "center",
              gap: "10px",
            }}
          >
            <IInput
              type="text"
              placeholder="Movie..."
              value={movieQuery}
              onChange={handleInputChange}
            />
            <Link to={`/search/movies/${movieQuery}`}>
              <IButton text="Search" />
            </Link>
          </div>

          <div
            style={{
              display: "flex",
              alignItems: "center",
              gap: "10px",
            }}
          >
            <IInput
              type="text"
              placeholder="Actor..."
              value={actorQuery}
              onChange={(e) => {
                setActorQuery(e.target.value);
              }}
            />

            <Link to={`/search/actors/${actorQuery}`}>
              <IButton text="Search" />
            </Link>
          </div>
        </div>
        <div className="Btn-menu">
          <button className="menu-login-button">
            <Link to={"/add_movies"} style={{ textDecoration: "none" }}>
              <h4 className="menu-login-text">Add Movies</h4>
            </Link>
          </button>
          <button className="menu-login-button">
            <Link to={"/add_actors"} style={{ textDecoration: "none" }}>
              <h4 className="menu-login-text">Add Actors</h4>
            </Link>
          </button>
        </div>
      </div>
      {/* )} */}
    </div>
  );
};

export default Appheader;
