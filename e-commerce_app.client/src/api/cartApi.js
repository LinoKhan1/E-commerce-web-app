// src/api/cartApi.js
import apiClient from './apiClient';

export const getCartItemsByUserId = async (userId) => {
    try {
        const response = await apiClient.get(`/api/cart/user/${userId}`);
        return response.data;
    } catch (error) {
        console.error('Failed to fetch cart items:', error);
        throw error;
    }
};

export const addToCart = async (addToCartDTO) => {
    try {
        const response = await apiClient.post('/api/cart', addToCartDTO);
        return response.data;
    } catch (error) {
        console.error('Failed to add item to cart:', error);
        throw error;
    }
};

export const updateCartItem = async (cartItemDTO) => {
    try {
        await apiClient.put('/api/cart', cartItemDTO);
    } catch (error) {
        console.error('Failed to update cart item:', error);
        throw error;
    }
};

export const deleteCartItem = async (cartItemId) => {
    try {
        await apiClient.delete(`/api/cart/${cartItemId}`);
    } catch (error) {
        console.error('Failed to delete cart item:', error);
        throw error;
    }
};
