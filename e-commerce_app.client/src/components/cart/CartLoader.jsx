// src/components/CartLoader.jsx
/*import React, { useEffect } from 'react';
import { useCart } from '../../hooks/useCart';

const CartLoader = () => {
    const { cartItems, loading, addToCart, updateCart, removeFromCart } = useCart();

    if (loading) {
        return <p>Loading cart...</p>;
    }

    return (
        <div>
            {cartItems.length === 0 ? (
                <p>Your cart is empty.</p>
            ) : (
                <ul>
                    {cartItems.map((item) => (
                        <li key={item.cartItemId}>
                            {item.productName} - {item.quantity}
                            <button onClick={() => updateCart(item.cartItemId, item.quantity + 1)}>+</button>
                            <button onClick={() => updateCart(item.cartItemId, item.quantity - 1)}>-</button>
                            <button onClick={() => removeFromCart(item.cartItemId)}>Remove</button>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};*/

export default CartLoader;
