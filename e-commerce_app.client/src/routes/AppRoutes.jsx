import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Shop from '../pages/Product/ShopPage.jsx';
const AppRoutes = () => {

    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/shop" element={<Shop/>}/>
            </Routes>
        </Router>
    );
};
export default AppRoutes;

