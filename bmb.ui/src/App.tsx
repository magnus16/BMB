import React from 'react';
import './App.css';
import { Navbar } from './components/navbar/navbar.component';
import { Navigate, Route, Routes } from 'react-router-dom';
import { MoviesPage } from './pages/movies/movies';
import { UsersPage } from './pages/users/users';
import { HomePage } from './pages/home/home-page';

function App() {
  return (

    <div className="App">
      <Navbar></Navbar>
      <div className="container pt-3">
        <Routes>
          <Route path='/' element={<HomePage />} />
          <Route path='/movies' element={<MoviesPage />} />
          <Route path='/users' element={<UsersPage />} />
          <Route path='*' element={<Navigate replace to="/" />} />
        </Routes>
      </div>
    </div>
  );
}

export default App;
