// src/context/AuthContext.js
import React, { createContext, useState } from 'react';
import { registerUser, loginUser } from '../services/authService';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [error, setError] = useState(null);

    const register = async (registerDTO) => {
        try {
            await registerUser(registerDTO);
            setError(null);
        } catch (error) {
            setError(error.message);
        }
    };

    const login = async (loginDTO) => {
        try {
            const data = await loginUser(loginDTO);
            setUser(data);
            setError(null);
        } catch (error) {
            setError(error.message);
        }
    };

    const logout = () => {
        setUser(null);  // Clear the user session
        localStorage.removeItem('authToken'); // Remove auth token from localStorage if used
    };

    return (
        <AuthContext.Provider value={{ user, register, login, logout, error }}>
            {children}
        </AuthContext.Provider>
    );
};
