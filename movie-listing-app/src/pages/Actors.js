import { useEffect, useState } from "react";
import { toast } from "react-toastify";
// import './profile.css';
import WebFont from "webfontloader";
import Card from "../components/Card/Card";
import styles from "../styles/general.module.css";
import { deleteActor, searchActor } from "../api/actors";
import { useParams } from "react-router-dom";

const Actors = () => {
  var [actors, setActors] = useState([]);
  const { query } = useParams();

  const handleDelete = async (id) => {
    await deleteActor(id).then((res) => {
      if (res === null) {
        // history.push("/actors");
        window.location.replace("/actors");
      }
    });
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const pageNumber = 1;
        const response = await fetch(
          `https://localhost:7133/api/ActorApi/get_all_Actors?pageNumber=${pageNumber}`,
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
          setActors((prevTeachers) => [
            ...prevTeachers,
            ...paginatedResult.pageItems,
          ]);
        } else {
          // toast.error("Error fetching actors: " + data.message);
          return data.message;
        }
      } catch (error) {
        // toast.error("Error fetching actors: " + error.message);
        return error.message;
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    if (query !== null && query !== "") {
      searchActor({ query }).then((res) => {
        if (res?.pageItems?.length > 0) {
          setActors(res?.pageItems);
        }
      });
    }
  }, [query]);
  return (
    <div>
      <div className={styles.container}>
        {actors
          ? actors?.map((actor) => (
              <Card
                key={actor?.actorId}
                name={actor?.actorName}
                viewLink={`/actors/${actor?.actorId}`}
                editLink={`/edit/actors/${actor?.actorId}`}
                deleteFunc={() => {
                  handleDelete(actor?.actorId);
                }}
              />
            ))
          : ""}
      </div>
    </div>
  );
};

export default Actors;
