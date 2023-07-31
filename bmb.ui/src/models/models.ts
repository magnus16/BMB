export interface Movie {
    title: string;
    description?: string;
    rating?: number;
    releaseDate?: Date;
    genre?: string;
    imageURL?: string;
    movieId: string;
    watched?: boolean;
    watchedOn?: Date;
    userId: string;
}

export interface UserLogin {
    username: string;
    password: string;
}


export interface User {
    isAuthenticated: boolean;
    username?: string;
    email?: string;
    userId?: string;
}

export interface SearchFilter {
    query?: string;
    genre?: number;
    year?: string;
}