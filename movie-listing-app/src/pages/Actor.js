import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import styles from "../styles/general.module.css";
import { getActor } from "../api/actors";

const Actor = () => {
  const { id } = useParams();
  const [details, setDetails] = useState();

  useEffect(() => {
    if (id !== undefined && id !== null) {
      getActor({ id }).then((res) => {
        if (res?.actorId) {
          setDetails(res);
        }
      });
    }
  }, [id]);
  return (
    <div className={styles.page_container}>
      <div
        style={{
          display: "flex",
          alignItems: "center",
        }}
      >
        <img src="/avatar.png" alt="" />

        <div>
          <h3 className={styles.title}>{details?.actorName}</h3>
          <span></span>
        </div>
      </div>
      <div style={{ marginTop: "30px" }}>
        <h2
          style={{
            fontSize: "30px",
          }}
        >
          Movies
        </h2>

        <div>
          <ul className="ul1">
            {details?.movies?.map((movie, index) => (
              <li className="li1" key={index}>
                {movie?.title} (Release year: {movie?.releaseYear})
              </li>
            ))}
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Actor;
