import axios, { AxiosRequestConfig } from "axios";
import { Movie, SearchFilter } from "../models/models";
import { store } from "../store";
import { setLoginStatus } from "../store/user";
const BASE_URL = "https://localhost:7207/api";


axios.defaults.baseURL = BASE_URL;
axios.defaults.withCredentials = true;

axios.interceptors.response.use(response => {
    return response;
}, error => {
    if (error.response.status === 401) {
        store.dispatch(setLoginStatus(false));
        window.location.reload();
    }
    return error;
});

const ApiService = {

    getMyMovies: () => axios.get<Movie[]>('/my'),
    loginUser: ({ username, password }: { username: string, password: string }) => axios.post('/account/login', { username, password }),
    getUser: () => axios.get<Movie[]>('/account'),
    watchMovie: (movieId: string, status: boolean) => axios.post('/my/ChangeWatchStatus/' + movieId),
    getMovies: () => axios.get('/movies'),
    newMovie: (movie: Movie) => axios.post('/movies/new', movie),
    addMovieToList: (movieId: string) => axios.post(`/my/AddMovie/${movieId}`),
    removeMovieFromList: (movieId: string) => axios.post(`/my/RemoveMovie/${movieId}`),
    logout: () => axios.get('/account/Logout'),
    register: (user: any) => axios.post('/account/AddUser', user),
    rateMovie: (movieId: string, rating: number) => axios.post(`/my/ratemovie/${movieId}/${rating}`),
    searchMovie: ((searchFilter: SearchFilter) => axios.post('/movies/search', searchFilter))
};

export default ApiService;