// src/context/cartContext.jsx
import React, { createContext, useState, useEffect } from 'react';
import { getCartItems, addToCart, updateCart, removeFromCart } from '../services/cartService';
import { useNavigate } from 'react-router-dom';

export const CartContext = createContext();

export const CartProvider = ({ children }) => {
    const [cartItems, setCartItems] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    // Helper function to check if the user is logged in
    const isUserLoggedIn = () => {
        const token = localStorage.getItem('token'); // Replace with your auth method
        return !!token; // Returns true if token exists, false otherwise
    };

    const fetchCartItems = async () => {
        if (!isUserLoggedIn()) {
            setError('User not logged in. Please login to view the cart.');
            return;
        }

        setLoading(true);
        setError(null);
        try {
            const data = await getCartItems();
            setCartItems(data);
        } catch (error) {
            console.error('Failed to fetch cart items:', error);
            setError(error);
        } finally {
            setLoading(false);
        }
    };

    const handleAddToCart = async (product) => {
        if (!isUserLoggedIn()) {
            setError('User not logged in. Please login to add items to the cart.');
            navigate('/login'); // Redirect to login
            return;
        }

        try {
            await addToCart(product);
            await fetchCartItems(); // Refresh cart items after adding
        } catch (error) {
            console.error('Failed to add item to cart:', error);
            setError(error);
        }
    };

    const handleUpdateCart = async (id, quantity) => {
        if (!isUserLoggedIn()) {
            setError('User not logged in. Please login to update the cart.');
            return;
        }

        try {
            await updateCart(id, quantity);
            await fetchCartItems(); // Refresh cart items after updating
        } catch (error) {
            console.error('Failed to update cart item:', error);
            setError(error);
        }
    };

    const handleRemoveFromCart = async (id) => {
        if (!isUserLoggedIn()) {
            setError('User not logged in. Please login to remove items from the cart.');
            return;
        }

        try {
            await removeFromCart(id);
            await fetchCartItems(); // Refresh cart items after removal
        } catch (error) {
            console.error('Failed to remove cart item:', error);
            setError(error);
        }
    };

    useEffect(() => {
        fetchCartItems(); // Load cart items on component mount
    }, []);

    return (
        <CartContext.Provider
            value={{
                cartItems,
                loading,
                error,
                fetchCartItems,
                handleAddToCart,
                handleUpdateCart,
                handleRemoveFromCart,
            }}
        >
            {children}
        </CartContext.Provider>
    );
};
