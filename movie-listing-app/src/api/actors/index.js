export const addActor = async (data) => {
  try {
    const response = await fetch(
      "https://localhost:7133/api/ActorApi/add_actor",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      }
    );
    var buffer = response;

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    console.log(JSON.stringify(data));
    console.log("yah1");
    var responseData = await response.json();
    // console.log(response);
    // console.log("Actor details submitted successfully");

    return responseData.data;
  } catch (error) {
    console.log(responseData);
    console.error("Error submitting Actor details:", error.message);
  }
  console.log(JSON.stringify(data));
  console.log("yah");

  // console.log(data);
  // console.log(responseData);
  // console.log("Actor details submitted:", data);
};

export const updateActor = async ({ id, data }) => {
  console.log(id, data);
  try {
    const response = await fetch(
      `https://localhost:7133/api/ActorApi/update_actor/${id}`,

      {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      }
    );

    const res = await response.json();
    console.log(res);

    if (res.status) {
      return res.data;
    } else {
      // toast.error("Error fetching actors: " + data.message);
      return res.message;
    }
  } catch (error) {
    // toast.error("Error fetching actors: " + error.message);
    console.log(error.message);
  }
};

export const deleteActor = async (id) => {
  try {
    const response = await fetch(
      `https://localhost:7133/api/ActorApi/delete_actor/${id}`,
      {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    const data = await response.json();
    console.log(data);

    if (data.status) {
      return data.data;
    } else {
      // toast.error("Error fetching actors: " + data.message);
      console.log(data.message);
    }
  } catch (error) {
    // toast.error("Error fetching actors: " + error.message);
    console.log(error.message);
  }
};

export const searchActor = async ({ query }) => {
  try {
    const pageNumber = 1;
    const response = await fetch(
      `https://localhost:7133/api/ActorApi/search_actor?pageNumber=${pageNumber}`,

      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ actorName: query }),
      }
    );

    const data = await response.json();
    console.log(data);

    if (data.status) {
      return data.data;
    } else {
      // toast.error("Error fetching actors: " + data.message);
      console.log(data.message);
    }
  } catch (error) {
    // toast.error("Error fetching actors: " + error.message);
    console.log(error.message);
  }
};

export const getActor = async ({ id }) => {
  console.log(id);
  try {
    const response = await fetch(
      `https://localhost:7133/api/ActorApi/get_actor_by_id/${id}`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    const data = await response.json();
    console.log(data);

    if (data.status) {
      return data.data;
    } else {
      // toast.error("Error fetching actors: " + data.message);
      console.log(data.message);
      // console.log(data.message);
    }
  } catch (error) {
    // toast.error("Error fetching actors: " + error.message);
    console.log(error.message);

    console.log(error.message);
  }
};
