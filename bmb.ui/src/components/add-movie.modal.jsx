import { useEffect, useState } from "react";
import ApiService from "../services/api.services";
import { Movie } from "../models/models";
import { Loader } from "./loader.component";
import { useFormik, Formik, Field, Form, FormikProvider } from "formik";
import { HttpStatusCode } from "axios";
import $ from 'jquery';

export const AddMovieModal = (props) => {

    let movie = {
        movieId: "",
        title: "",
        description: "",
        genre: "SciFi",
        imageURL: "",
        rating: 0,
    };
    useEffect(() => {

    }, []);

    let addMovie = async (movie) => {
        
        props.setLoading(true);
        ApiService.newMovie(movie).then(res => {
            if (res.status === HttpStatusCode.Ok) {
                $('#btn--close-movie-modal').trigger('click');
                props.getMovies();
            }
        }).catch(err => console.log(err))
            .finally(props.setLoading(false));
    }

    const formik = useFormik({
        initialValues: movie,
        onSubmit: values => {
            addMovie(values);
        },
    });

    function validateTitle(value) {
        let error;
        if (!value) {
            error = "Title is required."
            alert("Title is required.");
        }
        return error;
    }

    return (
        <FormikProvider value={formik}>
            <div className="modal fade" id="modal--add-movie">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h1 className="modal-title fs-5">New Movie</h1>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <form onSubmit={formik.handleSubmit}>
                            <div className="modal-body">
                                <div className="mb-3">
                                    <label className="form-label">Title</label>
                                    <Field type="text" name="title" className="form-control" validate={validateTitle} />
                                </div>
                                <div className="mb-3">
                                    <label className="form-label">Description</label>
                                    <Field type="text" name="description" className="form-control" />
                                </div>
                                <div className="mb-3">
                                    <label className="form-label">Genre</label>
                                    <Field className="form-select" name="genre" as="select">
                                        <option value="SciFi">Science Fiction</option>
                                        <option value="Action">Action</option>
                                        <option value="Drama">Drama</option>
                                    </Field>
                                </div>
                                <div className="mb-3">
                                    <label className="form-label">Image URL</label>
                                    <Field type="text" name="imageURL" className="form-control" />
                                </div>
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" id="btn--close-movie-modal">Close</button>
                                <button type="submit" className="btn btn-primary">Add Movie</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </FormikProvider >
    );

}
