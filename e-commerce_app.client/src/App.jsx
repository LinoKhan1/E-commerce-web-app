// src/App.jsx
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import React from 'react';
import Layout from './components/layout/layout.jsx';
import Home from './pages/Home/HomePage.jsx';
import Shop from './pages/Product/ShopPage.jsx';
import Cart from './pages/Cart/CartPage.jsx';
import LoginPage from './pages/Authentication/LoginPage.jsx';
import RegisterPage from './pages/Authentication/RegisterPage.jsx';
import { AuthProvider } from './context/AuthContext';
import { CartProvider } from './context/CartContext.jsx';

const App = () => {
    return (
        <BrowserRouter>
            <AuthProvider>
                    <Routes>
                        <Route path="/" element={<Layout />}>
                            <Route index element={<Home />} />
                            <Route path="shop" element={<Shop />} />
                            <Route path="shop/category/:categoryId" element={<Shop />} />
                            <Route path="cart" element={<Cart />} />
                            <Route path="login" element={<LoginPage />} />
                            <Route path="register" element={<RegisterPage />} />
                        </Route>
                    </Routes>
            </AuthProvider>
        </BrowserRouter>
    );
};

export default App;
