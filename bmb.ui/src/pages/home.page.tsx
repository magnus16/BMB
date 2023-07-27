import { useEffect, useState } from "react";
import ApiService from "../services/api.services";
import { Movie } from "../models/models";
import { Loader } from "../components/loader.component";
import { MovieCard } from "../components/movie-card.component";
import Layout from "../components/layout.component";

export const HomePage = () => {

    const [movies, setMovies] = useState<Movie[]>();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        ApiService.getMyMovies().then(res => {
            setMovies(res.data);
        }).catch(err => {
            //handle error
            console.log(err);
        }).finally(() => {
            setLoading(false)
        })
    }, []);


    return (
        <Layout>
            <div className="main">
                <Loader enabled={loading}></Loader>
                <h1 className="mb-3">My Watchlist!</h1>
                {movies && <div className="row movie-list">
                    {movies?.map((mov, i) => {
                        return <div key={i} data-movie-id={mov.movieId} className="col col-3">
                            <MovieCard showAddBtn={false} showRateBtn={true} showMarkWatchedBtn={true} movie={mov} showCTASection={true}></MovieCard>
                        </div>
                    })}
                </div>}
                {(!movies || movies.length===0)  && 
                <h3>You don't have any movies in your watchlist. <a href="/movies">Add some</a></h3>}
            </div>
        </Layout>

    );
}