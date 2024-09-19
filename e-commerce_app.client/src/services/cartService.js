// src/services/cartService.js
import { fetchCartItems, addCartItem, updateCartItem, removeCartItem } from "../api/cartApi";

export const getCartItems = async () => {
    try {
        const data = await fetchCartItems();
        return data;
    } catch (error) {
        console.error('Failed to fetch cart items:', error);
        throw error;
    }
};

export const addToCart = async (addToCartDto) => {
    try {
        await addCartItem(addToCartDto);
    } catch (error) {
        console.error('Failed to add cart item:', error);
        throw error;
    }
};

export const updateCart = async (id, quantity) => {
    try {
        await updateCartItem(id, quantity);
    } catch (error) {
        console.error('Failed to update cart item:', error);
        throw error;
    }
};

export const removeFromCart = async (id) => {
    try {
        await removeCartItem(id);
    } catch (error) {
        console.error('Failed to remove cart item:', error);
        throw error;
    }
};