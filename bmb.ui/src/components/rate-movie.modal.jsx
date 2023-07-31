import { useEffect, useState } from "react";
import ApiService from "../services/api.services";
import { HttpStatusCode } from "axios";
import $ from 'jquery';
import StarRatings from "./react-star-ratings/star-ratings"

export const RatingModal = (props) => {
    const [rating, setRating] = useState(0);
    useEffect(() => {
        setRating(props.movie?.rating || 0);
    })

    let changeRating = (newRating, name) => {
        setRating(newRating);
        props.setLoading(true);
        ApiService.rateMovie(props.movie?.movieId, newRating).then(res => {
            if (res.status === HttpStatusCode.Ok) {
                props.reload();
                alert(`You successfully rated ${props.movie?.title} ${newRating} stars`);
                $('#btn--close-rating-modal').trigger('click');
            }
        }).catch(err => {
            console.log(err);
            alert("There was an error in rating the movie");
        }).finally(() => props.setLoading(false));
    }

    return (
        <div className="modal fade" id="modal--rate-movie">
            <div className="modal-dialog modal-lg">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5">Rate {props.movie?.title}</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" id="btn--close-rating-modal"></button>
                    </div>
                    <div className="modal-body text-center">
                        <StarRatings
                            rating={rating}
                            starRatedColor="yellow"
                            changeRating={changeRating}
                            numberOfStars={10}
                            name='rating'
                        />
                    </div>
                </div>
            </div>
        </div>
    );

}
