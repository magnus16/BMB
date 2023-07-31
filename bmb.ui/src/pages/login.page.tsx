import React, { useEffect, useState } from "react";
import ApiService from "../services/api.services";
import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../store";
import { setLoginStatus, setUser } from "../store/user";
import { useNavigate } from "react-router-dom";


export const LoginPage = () => {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [loading, setLoading] = useState(false);
    const user = useSelector((state: RootState) => state.user);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        if (user.isAuthenticated) {
            navigate('/')
        }
    })


    let loginUser = async (e: any) => {
        e.preventDefault();
        setLoading(false);
        ApiService.loginUser({ username, password })
            .then(res => {
                dispatch(setLoginStatus(true))
                ApiService.getUser().then(res => {
                    if (res && res.data) {
                        dispatch(setUser({
                            ...res.data,
                            isAuthenticated: true
                        }));
                        navigate('/');
                    }
                })
            })
            .catch(err => console.log(err))
            .finally(() => setLoading(false));
    }
    return (
        <main className="w-100 mt-5 m-auto h-100 d-flex justify-content-center align-items-center">
            <form className=" mt-5 p-5 border bg-light" style={{ width: '500px' }} onSubmit={loginUser}>
                <h1 className="h3  mb-3 fw-normal">Please sign in</h1>
                <div className="mb-3">
                    <label className="form-label">Username</label>
                    <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} className="form-control" id="username" />
                </div>
                <div className="mb-3">
                    <label className="form-label">Password</label>
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} className="form-control" />
                </div>
                <button className="btn btn-primary w-100 py-2" type="submit">Sign in</button>
                <div className="text-center  opacity-75 m-3">OR</div>
                <div className="d-grid">
                    <a href="/register" className="btn btn-outline-primary btn-full">Register</a>
                </div>
            </form>
        </main>
    );
}