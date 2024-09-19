// src/api/authApi.js
import apiClient from "./apiClient";

export const register = async (registerDTO) => {
    try {
        const response = await apiClient.post('/api/auth/register', registerDTO);
        return response.data;
    } catch (error) {
        console.error('Failed to register:', error);
        throw error;
    }
};

export const login = async (loginDTO) => {
    try {
        const response = await apiClient.post('/api/auth/login', loginDTO);
        const { token } = response.data;
        if (token) {
            localStorage.setItem('token', token);
        }
        return response.data;
    } catch (error) {
        console.error('Failed to login:', error);
        throw error;
    }
};
