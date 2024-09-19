import React from "react";
import CartLoader from "../../components/cart/CartLoader";

const CartPage = () => {


    return (
        <>
            <div className="cart-page">
                <h1>Your Cart</h1>
                <CartLoader />
            </div>
        </>
    );
}

export default CartPage;