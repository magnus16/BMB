import axios from "axios";
import { Movie } from "../models/movie.model";
const BASE_URL = "https://localhost:7207/api";
const userToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjY0YmEyMjgwODc0OTZmZTc5MTNmNTgzMSIsInVuaXF1ZV9uYW1lIjoibG9rZXNoIiwibmJmIjoxNjkwMTgyNDM3LCJleHAiOjE2OTAxODk2MzcsImlhdCI6MTY5MDE4MjQzNywiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIwNy8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MjA3LyJ9.tDhbl6UY0D5bqFW0Mpx0X19ayvXtd9JDXVAN93r4jwc";


const ApiService = {
    loadDashboardData: function () {

        return axios.get<Movie[]>('/my',{
            baseURL:BASE_URL,
            headers:{
                Authorization: 'Bearer ' + userToken
            }
        });
    }
};

export default ApiService;