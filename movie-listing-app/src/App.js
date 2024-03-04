import "./App.css";
import Home from "./pages/Home.js";
import Movies from "./pages/Movies.js";
import Actors from "./pages/Actors.js";
import AddActors from "./pages/AddActors.js";
import AddMovies from "./pages/AddMovies.js";
import {
  BrowserRouter,
  Route,
  Switch,
  NavLink,
  Routes,
} from "react-router-dom";
import Appheader from "./Appheader";
import { ToastContainer } from "react-toastify";
import Appfooter from "./Appfooter";
import Actor from "./pages/Actor.js";
import Movie from "./pages/Movie.js";
import EditActor from "./pages/EditActor.js";

function App() {
  return (
    <div className="App">
      <ToastContainer></ToastContainer>
      <div className="page-container">
        <div className="content-wrap">
          <BrowserRouter>
            <Appheader />
            <Routes>
              <Route path="/" element={<Home />}></Route>

              <Route path="/actors" element={<Actors />}></Route>

              <Route path="/search/actors/:query" element={<Actors />}></Route>
              <Route path="/actors/:id" element={<Actor />}></Route>
              <Route path="/add_actors" element={<AddActors />}></Route>
              <Route path="/edit/actors/:id" element={<AddActors />}></Route>

              <Route path="/movies" element={<Movies />}></Route>
              <Route path="/movies/:id" element={<Movie />}></Route>
              <Route path="/search/movies/:query" element={<Movies />}></Route>
              <Route path="/movies/:id" element={<Movie />}></Route>

              <Route path="/edit/movies/:id" element={<AddMovies />}></Route>
              <Route path="/add_movies" element={<AddMovies />}></Route>
            </Routes>
            <Appfooter />
          </BrowserRouter>
        </div>
      </div>
    </div>
  );
}

export default App;
