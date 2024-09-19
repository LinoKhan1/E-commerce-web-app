// src/services/authService.js

import { register, login } from "../api/authApi";

export const registerUser = async (registerDTO) => {
    try {
        const data = await register(registerDTO);
        return data;
    } catch (error) {
        throw new Error('Registration failed');
    }
};

export const loginUser = async (loginDTO) => {
    try {
        const data = await login(loginDTO);
        return data;
    } catch (error) {
        throw new Error('Login failed');
    }
};