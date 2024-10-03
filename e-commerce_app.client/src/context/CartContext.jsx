// src/contexts/CartContext.js
/*import React, { createContext, useEffect, useState } from 'react';
import { useAuth } from '../hooks/useAuth'; // Assuming you have an Auth hook to check authentication
import cartService from '../services/cartService';

// Create a context for the cart
export const CartContext = createContext();

// Cart Provider component
export const CartProvider = ({ children }) => {
    const { isAuthenticated } = useAuth(); // Get authentication status from the auth hook
    const [cartItems, setCartItems] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    // Fetch cart items if the user is authenticated
    useEffect(() => {
        const fetchCartItems = async () => {
            if (isAuthenticated) {
                try {
                    const items = await cartService.getCartItems();
                    setCartItems(items);
                } catch (err) {
                    setError(err.message);
                } finally {
                    setLoading(false);
                }
            } else {
                setCartItems([]); // Reset cart items if not authenticated
                setLoading(false);
            }
        };

        fetchCartItems();
    }, [isAuthenticated]); // Run when authentication status changes

    // Function to add an item to the cart
    const addItemToCart = async (cartItem) => {
        if (!isAuthenticated) {
            throw new Error('User not authenticated');
        }
        await cartService.addItem(cartItem);
        const updatedCartItems = await cartService.getCartItems(); // Refresh cart items
        setCartItems(updatedCartItems);
    };

    // Function to update an item in the cart
    const updateCartItem = async (id, quantity) => {
        if (!isAuthenticated) {
            throw new Error('User not authenticated');
        }
        await cartService.updateItem(id, quantity);
        const updatedCartItems = await cartService.getCartItems(); // Refresh cart items
        setCartItems(updatedCartItems);
    };

    // Function to remove an item from the cart
    const removeCartItem = async (id) => {
        if (!isAuthenticated) {
            throw new Error('User not authenticated');
        }
        await cartService.removeItem(id);
        const updatedCartItems = await cartService.getCartItems(); // Refresh cart items
        setCartItems(updatedCartItems);
    };

    return (
        <CartContext.Provider value={{ cartItems, loading, error, addItemToCart, updateCartItem, removeCartItem }}>
            {children}
        </CartContext.Provider>
    );
};*/
