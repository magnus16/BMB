import React from 'react';
import './App.css';
import { Navbar } from './components/navbar.component';
import { Navigate, Route, Routes } from 'react-router-dom';
import { MoviesPage } from './pages/movies.page';
import { UsersPage } from './pages/users.page';
import { HomePage } from './pages/home.page';
import { LoginPage } from './pages/login.page';
import { RegisterPage } from './pages/register.page';
import { AuthGuard } from './guards/authguards';

function App() {

  return (

    <div className="App">
      <Routes>
        <Route path='/' element={<AuthGuard component={<HomePage />} />} />
        <Route path='/movies' element={<AuthGuard component={<MoviesPage />} />} />
        <Route path='/users' element={<AuthGuard component={<UsersPage />} />} />
        <Route path='/login' element={<LoginPage />} />
        <Route path='/register' element={<RegisterPage />} />
        <Route path='*' element={<Navigate replace to="/" />} />
      </Routes>
    </div>
  );
}

export default App;
