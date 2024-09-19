// src/context/AuthContext.js

import React, {createContext, useState} from "react";
import { registerUser, loginUser } from "../services/authService";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [error, setError] = useState(null);

    const register = async (registerDTO) => {
        try {
            const data = await registerUser(registerDTO);
            setUser(data);
        } catch (error) {
            setError(error.message);
        }
    };

    const login = async (loginDTO) => {
        try {
            const data = await loginUser(loginDTO);
            setUser(data);
        } catch (error) {
            setError(error.message);
        }
    };

    const logout = () => {
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, register, login, logout, error }}>
            {children}
        </AuthContext.Provider>
    );
};