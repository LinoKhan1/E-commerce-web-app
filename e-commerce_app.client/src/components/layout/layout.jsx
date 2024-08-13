import React from 'react';

import { Outlet, Link } from "react-router-dom";


const Layout = () => {
    return (
        <>
            <nav>
                <ul>
                    <li>
                        <Link to="/">Home</Link>
                    </li>
                    <li>
                        <Link to="products">Products</Link>
                    </li>
                    <li>
                       <Link to="cart">Cart</Link>
                    </li>
                </ul>
            </nav>
            <Outlet />
        </>
    )
};

export default Layout;