// src/utils/authUtils.js
import { decode } from 'jwt-decode';

// Function to get the user ID from the token
export const getUserIdFromToken = () => {
    const token = localStorage.getItem('token');
    if (!token) {
        return null; // Handle the case where there's no token
    }

    const decodedToken = decode(token); // Decode the JWT token
    return decodedToken?.userId; // Access the userId from the decoded token
};
