import { useState, useEffect } from "react";
import { addActor, getActor, updateActor } from "../api/actors";
import { useParams } from "react-router-dom";
import IButton from "../components/IButton/IButton";

const AddActors = () => {
  const { id } = useParams();
  const [stat, setStatus] = useState("");
  const [actorDetails, setActorDetails] = useState({
    actor: {
      firstName: "",
      lastName: "",
      birthYear: "",
    },
    movies: [],
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;

    if (name === "firstName" || name === "lastName" || name === "birthYear") {
      setActorDetails((prevDetails) => ({
        ...prevDetails,
        actor: {
          ...prevDetails.actor,
          [name]: value,
        },
      }));
    } else {
      setActorDetails((prevDetails) => ({
        ...prevDetails,
        [name]: value,
      }));
    }
  };

  const handleAddMovie = () => {
    const newMovie = {
      title: actorDetails.title,
      releaseYear: actorDetails.releaseYear,
    };

    setActorDetails((prevDetails) => ({
      ...prevDetails,
      movies: [...prevDetails.movies, newMovie],
    }));
  };

  const handleFormSubmit = async (e) => {
    e.preventDefault();

    if (id !== null && id !== undefined && actorDetails !== undefined) {
      updateActor({ id, actorDetails }).then((res) => {
        if (res?.actor?.actorId) {
          window.location.replace("/actors");
        }
      });
    } else {
      addActor(actorDetails).then((res) => {
        if (res !== undefined && res?.actorId) {
          window.location.replace("/actors");
        }
      });
    }
  };

  // set actor details if the actor's data is available
  useEffect(() => {
    if (id !== null && id !== undefined) {
      getActor({ id }).then((res) => {
        if (res !== undefined) {
          console.log(res);

          setActorDetails(res);
        }
      });
    }
  }, [id]);

  return (
    <div className="first-home-section1">
      <form className="form1" onSubmit={handleFormSubmit}>
        <label className="lab">
          Actor's First Name:
          <input
            className="input1"
            type="text"
            name="firstName"
            value={actorDetails?.firstName}
            onChange={handleInputChange}
          />
        </label>

        <label className="lab">
          Actor's Last Name:
          <input
            className="input1"
            type="text"
            name="lastName"
            value={actorDetails?.lastName}
            onChange={handleInputChange}
          />
        </label>

        <label className="lab">
          Actor's Birth Year:
          <input
            className="input1"
            type="text"
            name="birthYear"
            value={actorDetails?.birthYear}
            onChange={handleInputChange}
          />
        </label>

        <label className="lab">
          Movies:
          <ul className="ul1">
            {actorDetails?.movies?.map((movie, index) => (
              <li className="li1" key={index}>
                {movie?.title} (Release year: {movie?.releaseYear})
              </li>
            ))}
          </ul>
        </label>

        <div className="actoa">
          <label className="lab">
            Movie Title:
            <input
              className="input1"
              type="text"
              name="title"
              value={actorDetails?.title}
              onChange={handleInputChange}
            />
          </label>

          <label className="lab">
            Movie releaseYear:
            <input
              className="input1"
              type="text"
              name="releaseYear"
              value={actorDetails?.releaseYear}
              onChange={handleInputChange}
            />
          </label>
          <div className="btndiv">
            {/* <button className="btnadd" type="button" >
             
            </button> */}

            <IButton type="button" text="Add movie" onClick={handleAddMovie} />

            <IButton type="submit" text="Submit" />
            {/* <button className="btnsub" type="submit">
              Submit
            </button> */}
          </div>
        </div>
        {/* <label className="lab" >
                    {stat}
                </label> */}
      </form>
    </div>
  );
};

export default AddActors;
