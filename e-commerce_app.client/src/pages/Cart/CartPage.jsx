// src/pages/Cart/CartPage.jsx
import React from "react";
import CartLoader from "../../components/cart/CartLoader";
import { CartProvider } from "../../context/CartContext";

const CartPage = () => {


    return (
        <>
            <div className="cart-page">
                <h1>Your Cart</h1>
                <CartProvider>
                    <CartLoader />
                </CartProvider>
            </div>
        </>
    );
}

export default CartPage;