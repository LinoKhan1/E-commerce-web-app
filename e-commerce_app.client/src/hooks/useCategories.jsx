// src/hooks/useCategories.jsx

import { useContext } from 'react';
import { CategoryContext } from '../context/CategoryContext';

const useCategories = () => {
    const context = useContext(CategoryContext);
    if (!context) {
        throw new Error('useCategories must be used within a CategoryProvider');
    }
    return context;
};

export default useCategories;
