import React, { useEffect, useState } from "react";
import ApiService from "../services/api.services";
import { useDispatch, useSelector } from "react-redux";
import { setLoginStatus, setUser } from "../store/user";
import { useNavigate } from "react-router-dom";
import { ErrorMessage, Field, Form, Formik } from "formik";
import * as Yup from 'yup';
import { HttpStatusCode } from "axios";
import { RootState } from "../store";


export const RegisterPage = () => {
    const user = useSelector((state: RootState) => state.user);
    const [loading, setLoading] = useState(false);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        if (user.isAuthenticated) {
            navigate('/')
        }
    })

    let registerUser = async (values: any) => {
        setLoading(true);
        ApiService.register(values)
            .then(res => {
                if (res.status === HttpStatusCode.Ok) {
                    alert("You have been registered. Please login to continue.");
                    navigate('/login');
                }
            }).catch(err => console.log(err))
            .finally(() => setLoading(false))
    }

    const css = `
    .text-error-message{
        font-size:small;
    }
    .text-error-message:before{
        content:'  * ';
    }`;
    return (
        <>
            <style>
                {css}
            </style>
            <div className="row">
                <div className="col-4 offset-4">
                    <div className="mt-5">
                        <Formik
                            initialValues={{ username: '', email: '', password: '', confirmPassword: '' }}
                            validationSchema={Yup.object({
                                username: Yup.string().required('Required'),
                                email: Yup.string().email('Invalid email address').required('Required '),
                                password: Yup.string().required('Required'),
                                confirmPassword: Yup.string().test('passwords-match', 'Passwords must match', function (value) { return this.parent.password === value })
                            })}
                            onSubmit={registerUser}>
                            <Form className="mt-5 p-5 border bg-light">
                                <label className="form-label mb-2 mt-3" htmlFor="username">Username</label>
                                <ErrorMessage component="span" className="text-error-message text-danger" name="username" />
                                <Field className="form-control" name="username" type="text" />

                                <label className="form-label mb-2 mt-3" htmlFor="email">Email Address</label>
                                <ErrorMessage component="span" className="text-error-message text-danger" name="email" />
                                <Field className="form-control" name="email" type="email" />

                                <label className="form-label mb-2 mt-3" htmlFor="password">Password</label>
                                <ErrorMessage component="span" className="text-error-message text-danger" name="password" />
                                <Field className="form-control" name="password" type="password" />

                                <label className="form-label mb-2 mt-3" htmlFor="password">Confirm Password</label>
                                <ErrorMessage component="span" className="text-error-message text-danger" name="password" />
                                <Field className="form-control" name="confirmPassword" type="password" />
                                <div className="d-grid mt-3">
                                    <button className="btn btn-primary" type="submit">Submit</button>
                                    <div className="text-center  opacity-75 m-3">OR</div>
                                    <a href="/login" className="btn btn-outline-primary btn-full">Login</a>
                                </div>
                            </Form>
                        </Formik>
                    </div>
                </div>
            </div>
        </>
    );
}