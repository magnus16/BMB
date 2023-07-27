import { createSlice } from '@reduxjs/toolkit'
import { User } from '../models/models';
import { useNavigate } from 'react-router-dom';

const userState: User = localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user') || '{}') : { isAuthenticated: false };

const userSlice = createSlice({
    name: 'user',
    initialState: userState,
    reducers: {
        setLoginStatus: (state, action) => {
            // console.log({ state, action });
            state.isAuthenticated = action.payload;
            if (!state.isAuthenticated) {
                state.email = state.userId = state.username = undefined;
                localStorage.removeItem('user');
                
            }
        },
        setUser: (state, action) => {
            state.username = action.payload.username;
            state.email = action.payload.email;
            state.userId = action.payload.userId;
            localStorage.setItem('user', JSON.stringify(action.payload));
        }
    }
})


export const { setLoginStatus, setUser } = userSlice.actions;
export default userSlice.reducer;
