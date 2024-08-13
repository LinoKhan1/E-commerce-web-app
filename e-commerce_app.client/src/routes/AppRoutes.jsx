import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Products from '../pages/Product/ProductsPage.jsx';
const AppRoutes = () => {

    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/products" element={<Products/>}/>
            </Routes>
        </Router>
    );
};
export default AppRoutes;

