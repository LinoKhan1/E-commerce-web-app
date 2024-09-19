// src/api/cartApi.js
import apiClient from "./apiClient";

export const fetchCartItems = async () => {
    try {
        const response = await apiClient.get('api/cart');
        return response.data;
    } catch (error) {
        console.error('Failed to fetch cart items:', error);
        throw error;
    }
};

export const addCartItem = async (addToCartDto) => {
    try {
        await apiClient.post(`api/cart`, addToCartDto);
    } catch (error) {
        console.error('Failed to add cart item:', error);
        throw error;
    }
};

export const updateCartItem = async (id, quantity) => {
    try {
        await apiClient.put(`api/cart/${id}`, { quantity });
    } catch (error) {
        console.error('Failed to update cart item:', error);
        throw error;
    }
};

export const removeCartItem = async (id) => {
    try {
        await apiClient.delete(`api/cart/${id}`);
    } catch (error) {
        console.error('Failed to remove cart item:', error);
        throw error;
    }
};
