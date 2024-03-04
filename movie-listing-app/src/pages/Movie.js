import React, { useEffect, useState } from "react";
import styles from "../styles/general.module.css";
import { useParams } from "react-router-dom";
import { getMovie } from "../api/movies";

const Movie = () => {
  const { id } = useParams();
  const [details, setDetails] = useState();

  useEffect(() => {
    if (id !== undefined && id !== null) {
      getMovie({ id }).then((res) => {
        if (res?.id) {
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
        <img src="/film.png" alt="" />

        <div>
          <h3 className={styles.title}>{details?.title}</h3>
          <span></span>
        </div>
      </div>
    </div>
  );
};

export default Movie;
