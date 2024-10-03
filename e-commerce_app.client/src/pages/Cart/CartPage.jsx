// Example usage in a component
import React, { useEffect, useState } from 'react';
import { fetchCartItems } from '../../services/cartService';

const CartPage = () => {
    const [cartItems, setCartItems] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const getCartItems = async () => {
            try {
                const items = await fetchCartItems();
                setCartItems(items);
            } catch (err) {
                setError(err.message);
            }
        };

        getCartItems();
    }, []);

    if (error) return <div>Error: {error}</div>;

    return (
        <div>
            <h1>Your Cart</h1>
            {cartItems.length > 0 ? (
                <ul>
                    {cartItems.map(item => (
                        <li key={item.id}>{item.productName} - Quantity: {item.quantity}</li>
                    ))}
                </ul>
            ) : (
                <div>Your cart is empty</div>
            )}
        </div>
    );
};

export default CartPage;
