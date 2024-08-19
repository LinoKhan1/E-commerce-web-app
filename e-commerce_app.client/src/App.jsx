import { BrowserRouter, Routes, Route } from "react-router-dom";
import React from 'react';
import AppRoutes from './routes/AppRoutes';
import Layout from './components/layout/layout.jsx';
import Home from "./pages/Home/HomePage.jsx";
import Shop from "./pages/Product/ShopPage.jsx"
import Cart from "./pages/Cart/CartPage.jsx";

const App = () => {

    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Home />} />
                    <Route path="shop" element={<Shop />} />
                    <Route path="shop/category/:categoryId" element={<Shop />} />
                    <Route path="cart" element={<Cart/>}/>
                </Route>
            </Routes>
        </BrowserRouter>  
    );  
}
export default App;

