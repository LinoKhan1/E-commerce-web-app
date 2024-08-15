// src/hooks/useProduct.jsx

import { useContext } from 'react';
import { ProductContext } from '../context/ProductContext';

const useProducts = () => {

    const context = useContext(ProductContext);
    if (!context) {
        throw new Error('useProduct must be used within a ProductProvider');
    }
    return context;
};

export default useProducts;
