import styled, { css } from "styled-components";

const LoaderWrapper = styled.div`
        position: absolute;
        left: 0;
        right: 0;
        top: 0;
        bottom: 0;
        background-color: rgba(1,3,0,0.7);
    `;
export const Loader = (props: any) => {
    
    return (
        <>{props.enabled && <LoaderWrapper>
            <div className="c-loader d-flex align-items-center h-100">
                <div className="d-flex justify-content-center w-100">
                    <div className="spinner-border text-light" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                </div>
            </div>
        </LoaderWrapper>
        }
        </>
    );
}

