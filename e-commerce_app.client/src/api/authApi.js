// src/api/authApi.js

import apiClient from "./apiClient";

export const register = async (registerDTO) => {
    const response = await apiClient.post('/api/auth/register', registerDTO);
    return response.data;
};

export const login = async (loginDTO) => {
    const response = await apiClient.post('/api/auth/login', loginDTO);
    return response.data;
};
