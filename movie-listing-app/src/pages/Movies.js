import { useEffect, useState } from "react";
import { toast } from "react-toastify";
// import './profile.css';
import WebFont from "webfontloader";
import styles from "../styles/general.module.css";
import Card from "../components/Card/Card";
import { useParams } from "react-router-dom";
import { deleteMovie, searchMovie } from "../api/movies";

const Movies = () => {
  const { query } = useParams();
  var [movies, setMovies] = useState([]);

  const handleDeleteMovie = async (id) => {
    await deleteMovie(id).then((res) => {
      if (res === null) {
        // history.push("/actors");
        window.location.replace("/movies");
      }
    });
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const pageNumber = 1;
        const response = await fetch(
          `https://localhost:7133/api/MovieApi/get_all_movies?pageNumber=${pageNumber}`,
          {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
            },
          }
        );
        console.log(response);
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json();
        console.log(data);

        if (data.status) {
          const paginatedResult = data.data;
          console.log(paginatedResult.pageItems);
          setMovies((prevTeachers) => [
            ...prevTeachers,
            ...paginatedResult.pageItems,
          ]);
        } else {
          // toast.error('Error fetching movies: ' + data.message);
          return data.message;
        }
      } catch (error) {
        // toast.error('Error fetching movies: ' + error.message);
        return error.message;
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    if (query !== null && query !== "") {
      searchMovie({ query }).then((res) => {
        if (res?.pageItems?.length > 0) {
          setMovies(res?.pageItems);
        }
      });
    }
  }, [query]);
  return (
    <div>
      <div className={styles.container}>
        {movies?.map((movie) => (
          <Card
            key={movie?.id}
            name={movie?.title}
            viewLink={`/movies/${movie?.id}`}
            editLink={`/edit/movies/${movie?.id}`}
            rating={movie?.rating}
            deleteFunc={() => {
              handleDeleteMovie(movie?.id);
            }}
          />
        ))}

        <div className="first-section-second-container"></div>
      </div>
    </div>
  );
};

export default Movies;
