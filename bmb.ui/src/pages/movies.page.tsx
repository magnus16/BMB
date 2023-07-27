import { useEffect, useState } from "react";
import ApiService from "../services/api.services";
import { Movie } from "../models/models";
import { Loader } from "../components/loader.component";
import { MovieCard } from "../components/movie-card.component";
import Layout from "../components/layout.component";
import { AddMovieModal } from "../components/add-movie.modal";

export const MoviesPage = () => {

    const [movies, setMovies] = useState<Movie[]>();
    const [loading, setLoading] = useState(true);

    let getMovies = () => {
        ApiService.getMovies().then(res => {
            setMovies(res.data);
        }).catch(err => {
            //handle error
            console.log(err);
        }).finally(() => {
            setLoading(false)
        })
    }
    useEffect(() => {
        getMovies();
    }, []);


    return (
        <Layout>
            <div className="main">
                <h1 className="mb-3">
                    <button type="button" className="btn btn-primary float-end" data-bs-toggle="modal" data-bs-target="#modal--add-movie">
                        <i className="bi bi-plus-lg me-1"></i>
                        New Movie</button>
                    Movies!</h1>
                {movies && <div className="row movie-list">
                    {movies?.map((mov, i) => {
                        return <div key={i} data-movie-id={mov.movieId} className="col col-3">
                            <MovieCard showAddBtn={true} movie={mov} showCTASection={true}></MovieCard>
                        </div>
                    })}
                </div>}
                {!movies && <h3>No movies. Try adding some.</h3>}
            </div>
            <AddMovieModal setLoading={setLoading} getMovies={getMovies} />
            <Loader enabled={loading}></Loader>
        </Layout>

    );
}