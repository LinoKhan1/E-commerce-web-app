// src/api/apiClient.js
import axios from 'axios';

// Function to get the token form localStorage 
const getToken = () => {
    return localStorage.getItem('token');
};



// Create an Axios instance
const apiClient = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    headers:{
        'content-Type':'application/json',
    },
});

// Add a request interceptor to attach the token
apiClient.interceptors.request.use(
    (config) => {
        const token = getToken();
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
)
export default apiClient;
