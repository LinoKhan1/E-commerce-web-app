import React, { createContext, useState, useEffect } from 'react';
import { getCartItems, addToCart, updateCart, removeFromCart } from '../services/cartService'; // Adjust the import path as necessary

export const CartContext = createContext();

export const CartProvider = ({ children }) => {
    const [cartItems, setCartItems] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const fetchCartItems = async () => {
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
        try {
            await addToCart(product);
            await fetchCartItems(); // Refresh cart items after adding
        } catch (error) {
            console.error('Failed to add item to cart:', error);
            setError(error);
        }
    };

    const handleUpdateCart = async (id, quantity) => {
        try {
            await updateCart(id, quantity);
            await fetchCartItems(); // Refresh cart items after updating
        } catch (error) {
            console.error('Failed to update cart item:', error);
            setError(error);
        }
    };

    const handleRemoveFromCart = async (id) => {
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
            value={{ cartItems, loading, error, addToCart: handleAddToCart, updateCart: handleUpdateCart, removeFromCart: handleRemoveFromCart }}
        >
            {children}
        </CartContext.Provider>
    );
};
