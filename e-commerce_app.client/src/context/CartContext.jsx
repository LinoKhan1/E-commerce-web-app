// src/context/CartContext.jsx
/*import React, { createContext, useState, useEffect } from 'react';
import useAuth from '../hooks/useAuth'; // Correct import for default export
import { getCartItems, addItemToCart, updateToCart, deleteFromCart } from '../services/cartService';
import { useNavigate } from 'react-router-dom';

const CartContext = createContext();

export const CartProvider = ({ children }) => {
    const [cartItems, setCartItems] = useState([]);
    const [loading, setLoading] = useState(true);
    const { isAuthenticated } = useAuth(); // Authentication state
    const navigate = useNavigate();

    useEffect(() => {
        if (isAuthenticated) {
            fetchCartItems();
        } else {
            navigate('/login'); // Redirect to login if not authenticated
        }
    }, [isAuthenticated, navigate]);

    const fetchCartItems = async () => {
        try {
            setLoading(true);
            const items = await getCartItems();
            setCartItems(items);
        } catch (error) {
            console.error("Failed to fetch cart items:", error);
        } finally {
            setLoading(false);
        }
    };

    const addToCart = async (productId, quantity) => {
        if (!isAuthenticated) {
            navigate('/login');
            return;
        }
        try {
            await addItemToCart({ productId, quantity });
            await fetchCartItems();
        } catch (error) {
            console.error("Failed to add item to cart:", error);
        }
    };

    const updateCart = async (cartItemId, quantity) => {
        if (!isAuthenticated) {
            navigate('/login');
            return;
        }
        try {
            await updateToCart(cartItemId, quantity);
            await fetchCartItems();
        } catch (error) {
            console.error("Failed to update cart item:", error);
        }
    };

    const removeFromCart = async (cartItemId) => {
        if (!isAuthenticated) {
            navigate('/login');
            return;
        }
        try {
            await deleteFromCart(cartItemId);
            await fetchCartItems();
        } catch (error) {
            console.error("Failed to remove item from cart:", error);
        }
    };

    return (
        <CartContext.Provider
            value={{
                cartItems,
                addToCart,
                updateCart,
                removeFromCart,
                loading,
            }}
        >
            {children}
        </CartContext.Provider>
    );
};
*/