export const addMovie = async (data) => {
  try {
    const response = await fetch(
      "https://localhost:7133/api/MovieApi/add_movie",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      }
    );

    if (!response.ok) {
      console.log(JSON.stringify(data));
      console.log("error occured");
    }
    console.log("Movie details submitted successfully");
    var responseData = await response.json();
    return responseData?.data;
  } catch (error) {
    console.error("Error submitting movie details:", error.message);
  }
};
export const getAllMovie = async (id) => {
  try {
    const response = await fetch(
      `https://localhost:7133/api/MovieApi/get_all_movies?pageNumber=${1}`,
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
      return data?.data;
    } else {
      // toast.error('Error fetching movies: ' + data.message);
      console.log(data.message);
    }
  } catch (error) {
    // toast.error('Error fetching movies: ' + error.message);
    console.log(error.message);
  }
};

export const searchMovie = async ({ query }) => {
  try {
    const response = await fetch(
      `https://localhost:7133/api/MovieApi/search_movie?pageNumber${1}`,

      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ movieName: query }),
      }
    );
    console.log(response);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    const data = await response.json();
    console.log(data);

    if (data.status) {
      return data.data;
    } else {
      // toast.error('Error fetching movies: ' + data.message);
      console.log(data.message);
    }
  } catch (error) {
    // toast.error('Error fetching movies: ' + error.message);
    console.log(error.message);
  }
};

export const getMovie = async ({ id }) => {
  try {
    const response = await fetch(
      `https://localhost:7133/api/MovieApi/get_movie_by_id/${id}`,
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
      return data?.data;
    } else {
      // toast.error('Error fetching movies: ' + data.message);
      console.log(data.message);
    }
  } catch (error) {
    // toast.error('Error fetching movies: ' + error.message);
    console.log(error.message);
  }
};

export const deleteMovie = async (id) => {
  try {
    const response = await fetch(
      `https://localhost:7133/api/MovieApi/delete_movie/${id}`,
      {
        method: "DELETE",
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
      return data?.data;
    } else {
      // toast.error('Error fetching movies: ' + data.message);
      console.log(data.message);
    }
  } catch (error) {
    // toast.error('Error fetching movies: ' + error.message);
    console.log(error.message);
  }
};

export const updateMovie = async ({ id, data }) => {
  try {
    const response = await fetch(
      `https://localhost:7133/api/MovieApi/update_movie/${id}`,
      {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      }
    );
    console.log(response);
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    const res = await response.json();
    console.log(res);

    if (res.status) {
      return res?.data;
    } else {
      // toast.error('Error fetching movies: ' + data.message);
      console.log(res.message);
    }
  } catch (error) {
    // toast.error('Error fetching movies: ' + error.message);
    console.log(error.message);
  }
};
