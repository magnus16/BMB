import { CSSProperties, useEffect, useState } from 'react';
import dummyPosterImg from '../images/generic-movie.png';
import ApiService from '../services/api.services';
import { HttpStatusCode } from 'axios';
import { Movie } from '../models/models';

// import styled, { css } from "styled-components";
// const LoaderWrapper = styled.div`
//         position: absolute;
//         left: 0;
//         right: 0;
//         top: 0;
//         bottom: 0;
//         background-color: rgba(1,3,0,0.7);
//     `;

export const MovieCard = (props: any) => {
    const [movie, setMovie] = useState<Movie>();
    const [watched, setWatched] = useState(false);
    const [isMovieInList, setIsMovieInList] = useState(false)
    useEffect(() => {
        setMovie(props.movie);
        setWatched(props.movie?.watched);
        setIsMovieInList(props.movie?.userId != null)

    }, []);

    const imageStyle: CSSProperties = {
        width: '100%',
        height: '400px',
        objectFit: "cover",
    };
    const ctaWrapper: CSSProperties = {

    };

    let changeMovieStatus = async (e: any) => {
        if (movie != null) {
            ApiService.watchMovie(movie.movieId, !watched).then((res) => {
                if (res.status === HttpStatusCode.Ok) {
                    setWatched(!watched);
                }
            });
        }
    }

    let addMovieToUserList = async (e: any) => {
        if (movie != null) {
            ApiService.addMovieToList(movie.movieId).then((res) => {
                if (res.status === HttpStatusCode.Ok) {
                    setIsMovieInList(true);
                }
            });
        }
    }
    let removeMovieFromUserList = async (e: any) => {
        if (movie != null) {
            ApiService.removeMovieFromList(movie.movieId).then((res) => {
                if (res.status === HttpStatusCode.Ok) {
                    setIsMovieInList(false);
                }
            });
        }
    }

    let rateMovie = () => {
        props.setMovieToBeRated(props.movie);
    }
    return (
        <div className="movie-card mb-3">
            <div className="card d-flex flex-column" style={{ position: 'relative' }}>
                {props.movie.genre && <div className="ribbon ribbon-genre">{props.movie.genre}</div>}
                {props.movie.rating &&
                    <div className="bg-warning ribbon ribbon-rating">
                        {props.parent === 'home' ? <span>You rated </span> : <span>Avg. Rating </span>}
                        {props.movie.rating} <i className="bi bi-star-fill"></i>
                    </div>
                }
                <img style={imageStyle} src={props.movie.imageURL ? props.movie.imageURL : dummyPosterImg} className="card-img-top" />
                <div className="card-body">
                    <h5 className="card-title">{props.movie.title} </h5>
                    {props.movie.description && <p className="card-text">
                        {props.movie.description}
                    </p>}


                    {
                        props.showCTASection
                        &&
                        <div className='cta-wrapper d-flex justify-content-center' style={ctaWrapper}>
                            {
                                props.showAddBtn ?
                                    isMovieInList ? <button type='button' onClick={removeMovieFromUserList} className='btn btn-sm btn-outline-primary'>
                                        <i className="bi bi-x-lg me-1"></i>
                                        Remove Movie
                                    </button> : <button type='button' onClick={addMovieToUserList} className='btn btn-sm btn-outline-primary'>
                                        <i className="bi bi-plus-lg me-1"></i>
                                        Add Movie
                                    </button> :
                                    <></>
                            }
                            {
                                props.showMarkWatchedBtn ?
                                    <button type='button' onClick={changeMovieStatus}  className='btn btn-sm btn-outline-primary'>
                                        {watched && <>
                                            <i className="bi bi-eye-slash-fill  me-1"></i>
                                            <span>Mark as unwatched</span>
                                        </>}
                                        {!watched && <>
                                            <i className="bi bi-eye-fill  me-1"></i>
                                            <span>Mark as watched</span>
                                        </>}
                                    </button>
                                    : <></>
                            }
                            {
                                props.showRateBtn ?//&& !props.movie.rating ?
                                    <button type='button' className='ms-auto btn btn-sm btn-warning' onClick={rateMovie} data-bs-toggle="modal" data-bs-target="#modal--rate-movie">
                                        <i className="bi bi-star-fill me-1"></i>
                                        Rate
                                    </button> :
                                    <></>
                            }
                        </div>
                    }
                </div>
            </div>
        </div>
    );
}

