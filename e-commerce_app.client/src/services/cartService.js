// src/services/cartService.js
import { getUserIdFromToken } from '../utils/authUtils';
import { getCartItemsByUserId, addToCart, updateCartItem, deleteCartItem } from '../api/cartApi';

export const fetchCartItems = async () => {
    const userId = getUserIdFromToken();
    if (!userId) {
        throw new Error('User ID not found in token');
    }
    return await getCartItemsByUserId(userId);
};

export const addItemToCart = async (productId, quantity) => {
    const userId = getUserIdFromToken();
    if (!userId) {
        throw new Error('User ID not found in token');
    }
    const addToCartDTO = { userId, productId, quantity }; // Adjust DTO according to your backend expectations
    return await addToCart(addToCartDTO);
};

export const updateCart = async (cartItemDTO) => {
    return await updateCartItem(cartItemDTO);
};

export const removeCartItem = async (cartItemId) => {
    return await deleteCartItem(cartItemId);
};
