import { Navbar } from "./navbar.component";
import { ReactNode } from "react";

type LayoutProps = {
    children?: ReactNode
}

const Layout = ({ children, ...props }: LayoutProps) => {
    
    return (
        <>
            <Navbar></Navbar>
            <div className="container pt-3">
                {children}
            </div>
        </>

    );
}

export default Layout;