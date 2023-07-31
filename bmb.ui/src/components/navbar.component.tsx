import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
import { RootState } from "../store";
import { useSelector } from "react-redux";
import { store } from "../store/index";
import { setLoginStatus } from "../store/user";

export const NavMenu = (props: any) => {
    const user = useSelector((state: RootState) => state.user);
    const navigate = useNavigate();
    const location = useLocation();
    const { pathname } = location;


    function getClassName(url: string) {
        let sploc = "/" + pathname.split("/")[1];
        let className = "nav-link ";
        if ((sploc === "/" && url === "/home") || (sploc == url)) {
            className += " active";
        }
        return className;
    }

    let logout = () => {
        store.dispatch(setLoginStatus(false));
        navigate('/login');
    }

    // let listItems = [];
    // for (let l of props.links) {
    //     listItems.push(
    //         <li className="nav-item">
    //             <a className="nav-link active" href={l.url}>{l.name}</a>
    //         </li>
    //     );
    // }


    return (
        <div className="collapse navbar-collapse" id="navbarNav" >
            <ul className="navbar-nav me-auto">
                {props.links.map((l: any, i: number) => {
                    return (
                        <li key={i} className="nav-item">
                            <Link to={l.url} className={getClassName(l.url)}>{l.name}</Link>
                        </li>
                    );
                })}
            </ul>
            <form className="d-flex">
                <div className="nav-item dropdown">
                    <button className="btn btn-light
                     dropdown-toggle" data-bs-toggle="dropdown">
                        {user.username}
                    </button>
                    <ul className="dropdown-menu">
                        <li><a className="dropdown-item" href="#" onClick={logout}>Logout</a></li>
                    </ul>
                </div>
            </form>
        </div>
    );
}


const links = [
    { name: 'Home', url: '/home' },
    { name: 'Movies', url: '/movies' },
    // { name: 'Users', url: '/users' },
];

export const Navbar = () => {
    return (
        <nav className="navbar navbar-expand-lg bg-body-tertiary">
            <div className="container">
                <a className="navbar-brand" href="#">BMB</a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <NavMenu links={links}></NavMenu>
            </div>
        </nav >
    );
}

