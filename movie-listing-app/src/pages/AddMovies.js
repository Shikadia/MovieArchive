import { useState, useEffect } from "react";
import { addMovie, getMovie, updateMovie } from "../api/movies";
import { useParams } from "react-router-dom";
import IButton from "../components/IButton/IButton";

const AddMovies = () => {
  const { id } = useParams();
  const [stats, setstats] = useState("");
  const [movieDetails, setMovieDetails] = useState({
    movie: {
      title: "",
      releaseYear: "",
    },
    actors: [],
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;

    if (name === "releaseYear" || name === "title") {
      setMovieDetails((prevDetails) => ({
        ...prevDetails,
        movie: {
          ...prevDetails.movie,
          [name]: value,
        },
      }));
    } else {
      setMovieDetails((prevDetails) => ({
        ...prevDetails,
        [name]: value,
      }));
    }
  };

  const handleAddActor = () => {
    const newActor = {
      firstName: movieDetails.firstName,
      lastName: movieDetails.lastName,
      birthYear: movieDetails.birthYear,
    };

    setMovieDetails((prevDetails) => ({
      ...prevDetails,
      actors: [...prevDetails.actors, newActor],
    }));

    console.log(movieDetails);
  };

  const handleFormSubmit = async (e) => {
    e.preventDefault();
    if (id !== null && id !== undefined && movieDetails !== undefined) {
       updateMovie({ id, movieDetails }).then((res) => {
        if (res?.movie?.movieId) {
          window.location.replace("/movies");
        }
        console.log(movieDetails);
      });
    } else {
       addMovie({ movieDetails }).then((res) => {
        if (res !== undefined && res?.movieId) {
          window.location.replace("/movies");
        }
        console.log(movieDetails);
      });
    }

    // console.log("Movie details submitted:", movieDetails);
  };

  useEffect(() => {
    if (id !== null && id !== undefined) {
      getMovie({ id }).then((res) => {
        if (res !== undefined) {
          setMovieDetails(res);
        }
      });
    }
  }, [id]);
  return (
    <div className="first-home-section1">
      <form className="form1" onSubmit={handleFormSubmit}>
        <label className="lab">
          Movie Name:
          <input
            className="input1"
            type="text"
            name="title"
            value={movieDetails?.Title}
            onChange={handleInputChange}
          />
        </label>

        <label className="lab">
          Year of Release:
          <input
            className="input1"
            type="text"
            name="releaseYear"
            value={movieDetails?.ReleaseYear}
            onChange={handleInputChange}
          />
        </label>

        <label className="lab">
          Actors:
          <ul className="ul1">
            {movieDetails?.actors?.map((actor, index) => (
              <li className="li1" key={index}>
                {actor.FirstName} (Born: {actor.BirthYear})
              </li>
            ))}
          </ul>
        </label>

        <div className="actoa">
          <label className="lab">
            Actor first name:
            <input
              className="input1"
              type="text"
              name="firstName"
              value={movieDetails?.FirstName}
              onChange={handleInputChange}
            />
          </label>

          <label className="lab">
            Actor last name:
            <input
              className="input1"
              type="text"
              name="lastName"
              value={movieDetails?.LastName}
              onChange={handleInputChange}
            />
          </label>

          <label className="lab">
            Actor Birth Year:
            <input
              className="input1"
              type="text"
              name="birthYear"
              value={movieDetails?.BirthYear}
              onChange={handleInputChange}
            />
          </label>
          <div className="btndiv">
            {/* <button className="btnadd" type="button" onClick={handleAddActor}>
              Add Actor
            </button> */}

            <IButton type="button" text="Add Actor" onClick={handleAddActor} />

            <IButton type="submit" text="Submit" />
          </div>
        </div>
        {/* <label className="lab">
                   {stats}
                </label> */}
      </form>
    </div>
  );
};

export default AddMovies;
