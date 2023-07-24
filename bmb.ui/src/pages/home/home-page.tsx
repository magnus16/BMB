// import React from "react";
import { useEffect, useState } from "react";
import axios from "axios";
import ApiService from "../../services/api.services";
import { Movie } from "../../models/movie.model";
import { Loader } from "../../components/loader/loader.component";
export const HomePage = () => {

    const [movies, setMovies] = useState<Movie[]>();
    const [loading, setLoading] = useState(true);

    const userToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjY0YmEyMjgwODc0OTZmZTc5MTNmNTgzMSIsInVuaXF1ZV9uYW1lIjoibG9rZXNoIiwibmJmIjoxNjkwMTg3NDcxLCJleHAiOjE2OTAxOTQ2NzEsImlhdCI6MTY5MDE4NzQ3MSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIwNy8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MjA3LyJ9.ZJRVJk7uTZd71OFhvSAycBx7o0mpBVlZam9R32W2QWo";
    useEffect(() => {
        ApiService.loadDashboardData().then(res => {
            console.log(movies);
            setMovies(res.data);
        }).catch(err => {
            //handle error
            console.log(err);
        }).finally(() => {
            
            setLoading(false)
        })
    }, []);

    return (
        <div className="main">
            <Loader enabled={loading}></Loader>
            <h1>My Watchlist!</h1>

            {JSON.stringify(movies, null, 5)}
        </div>
    );
}