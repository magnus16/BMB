import { useEffect, useState } from "react";
import ApiService from "../services/api.services";
import { Movie, SearchFilter } from "../models/models";
import { Loader } from "../components/loader.component";
import { MovieCard } from "../components/movie-card.component";
import Layout from "../components/layout.component";
import { AddMovieModal } from "../components/add-movie.modal";
export const MoviesPage = () => {

    const [movies, setMovies] = useState<Movie[]>();
    const [loading, setLoading] = useState(true);
    const [years, setYears] = useState<number[]>([]);
    const [query, setQuery] = useState("");
    const [genre, setGenre] = useState("");
    const [year, setYear] = useState("");

    useEffect(() => {
        getMovies();
    }, []);

    let clearFilters = () => {
        setQuery("");
        setGenre("");
        setYear("");
        getMovies();
    };

    let getMovies = () => {
        ApiService.getMovies().then(res => {
            setMovies(res.data);
            setYears(
                res.data.filter((m: any) => m.releaseDate)
                    .map((m: any) => (new Date(m.releaseDate).getFullYear()))
                    .sort((a: number, b: number) => { return b - a })
            );

        }).catch(err => {
            //handle error
            console.log(err);
        }).finally(() => {
            setLoading(false)
        });
    }
    let searchMovie = () => {
        let searchFilter: SearchFilter = {
            genre: (genre && genre.length > 0) ? parseInt(genre) : undefined,
            query: query,
            year: (year && year.length === 4) ? year : undefined
        };
        console.log(searchFilter);
        ApiService.searchMovie(searchFilter).then(res => {
            setMovies(res.data);
        }).catch(err => {
            console.log(err);
        }).finally(() => {
            setLoading(false)
        });
    }


    return (
        <Layout>
            <div className="main">

                <div className="d-flex justify-content-between mb-3">
                    <div className="d-flex gap-2 w-75">
                        <div className="d-flex w-50">
                            <input className="form-control" type="search" onChange={e => setQuery(e.target.value)} placeholder="Search" value={query} name="query" />
                        </div>
                        <div className="area-genre">
                            <select className="form-select" value={genre} name="genre" onChange={e => setGenre(e.target.value)} >
                                <option value={undefined}>Select Genre</option>
                                <option value="0">Science Fiction</option>
                                <option value="1">Action</option>
                                <option value="2">Drama</option>
                            </select>
                        </div>
                        <div className="area-year">
                            <select className="form-select" value={year} name="year" onChange={e => setYear(e.target.value)}>
                                <option value={undefined}>Select Year</option>
                                {years.map((y: number, i: any) => <option key={i} value={y}>{y}</option>)}
                            </select>
                        </div>
                        <button className="btn btn-light" onClick={clearFilters}>Clear</button>
                        <button className="btn btn-outline-success" onClick={searchMovie} type="submit">Search</button>
                    </div>
                    <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modal--add-movie">
                        <i className="bi bi-plus-lg me-1"></i>
                        New Movie
                    </button>
                </div>
                {movies && <div className="row movie-list">
                    {movies?.map((mov, i) => {
                        return <div key={i} data-movie-id={mov.movieId} className="col col-3">
                            <MovieCard showAddBtn={true} parent={'movies'} movie={mov} showCTASection={true}></MovieCard>
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