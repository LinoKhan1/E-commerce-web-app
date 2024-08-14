import axios from 'axios';

// Create an Axios instance
const apiClient = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    headers:{
        'content-Type':'application/json',
    },
});
export default apiClient;