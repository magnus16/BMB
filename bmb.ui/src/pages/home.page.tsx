import { useEffect, useState } from "react";
import ApiService from "../services/api.services";
import { Movie } from "../models/models";
import { Loader } from "../components/loader.component";
import { MovieCard } from "../components/movie-card.component";
import Layout from "../components/layout.component";
import { RatingModal } from "../components/rate-movie.modal";

export const HomePage = () => {

    const [movies, setMovies] = useState<Movie[]>();
    const [loading, setLoading] = useState(true);
    const [movieToBeRated, setMovieToBeRated] = useState(null);
    useEffect(() => {
        getMyMovies();
    }, []);

    let getMyMovies = () => {
        ApiService.getMyMovies().then(res => {
            setMovies(res.data);
        }).catch(err => {
            //handle error
            console.log(err);
        }).finally(() => {
            setLoading(false)
        })
    }

    return (
        <Layout>
            <div className="main">
                <Loader enabled={loading}></Loader>
                <h1 className="mb-3">My Watchlist!</h1>
                {movies && <div className="row movie-list">
                    {movies?.map((mov, i) => {
                        return <div key={i} data-movie-id={mov.movieId} className="col col-3">
                            <MovieCard setMovieToBeRated={setMovieToBeRated} showAddBtn={false} showRateBtn={true} parent={"home"} showMarkWatchedBtn={true} movie={mov} showCTASection={true}></MovieCard>
                        </div>
                    })}
                </div>}
                {(!movies || movies.length === 0) &&
                    <h3>You don't have any movies in your watchlist. <a href="/movies">Add some</a></h3>}
            </div>
            <RatingModal setLoading={setLoading} movie={movieToBeRated} reload={getMyMovies} />
        </Layout>

    );
}