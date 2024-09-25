// src/api/cartApi.js
import apiClient from './apiClient';

export const fetchCartItems = async () => {
    try {
        const response = await apiClient.get('/api/cart');
        return response.data;
    } catch (error) {
        throw new Error(`Failed to fetch cart items: ${error.message}`);
    }
};

export const addCartItem = async (cartItem) => {
    try {
        const response = await apiClient.post('/api/cart', cartItem);
        return response.data;
    } catch (error) {
        throw new Error(`Failed to add item to cart: ${error.message}`);
    }
};

export const updateCartItem = async (id, quantity) => {
    try {
        await apiClient.put(`/api/cart/${id}`, { quantity });
    } catch (error) {
        throw new Error(`Failed to update cart item: ${error.message}`);
    }
};

export const removeCartItem = async (id) => {
    try {
        await apiClient.delete(`/api/cart/${id}`);
    } catch (error) {
        throw new Error(`Failed to remove cart item: ${error.message}`);
    }
};
