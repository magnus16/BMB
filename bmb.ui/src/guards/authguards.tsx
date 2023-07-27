import React, { ReactNode, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { RootState } from '../store';
import { useNavigate } from 'react-router-dom';

export const AuthGuard = ({ component }: { component: ReactNode }) => {
    const user = useSelector((state: RootState) => state.user);
    const [status, setStatus] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        checkToken();
    }, [component]);

    const checkToken = async () => {
        try {
            if (!user.isAuthenticated) {
                navigate(`/login`);
            }
            setStatus(true);
            return;
        } catch (error) {
            console.log(error);
            navigate(`/login`);
        }
    }

    return status ? <React.Fragment>{component}</React.Fragment> : <React.Fragment></React.Fragment>;

}

// export const UnAuthGuard = ({ component }: { component: ReactNode }) => {
//     useEffect(() => {
//         console.log("UnAuth Guard");
//     }, [component]);

//     return <React.Fragment>{component}</React.Fragment>
// }

