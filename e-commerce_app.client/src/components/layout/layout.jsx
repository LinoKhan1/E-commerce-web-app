import React from 'react';
import { Outlet} from "react-router-dom";

import Navbar from "./navbar.jsx";
import Footer from "./footer.jsx";

const Layout = () => {
    return (
        <>
            <Navbar />
            <Outlet />
            <Footer/>
        </>
    )
};

export default Layout;