import React, { createContext, useState } from 'react';
import { fetchLimitedProducts, fetchProductByCategory } from '../services/productService';

export const ProductContext = createContext();

export const ProductProvider = ({ children }) => {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const getAllProducts = async () => {
        setLoading(true);
        setError(null);
        try {
            const data = await fetchLimitedProducts(); // Fetch all products
            console.log("Fetched products:", data); // Log API response
            setProducts(data);
        } catch (error) {
            console.error('Failed to fetch products:', error);
            setError(error);
        } finally {
            setLoading(false);
        }
    };

    const getProductsByCategory = async (categoryId) => {
        setLoading(true);
        setError(null);
        try {
            const data = await fetchProductByCategory(categoryId);
            setProducts(data);
        } catch (error) {
            console.error('Failed to fetch products by category:', error);
            setError(error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <ProductContext.Provider
            value={{ products, loading, error, getAllProducts, getProductsByCategory }}
        >
            {children}
        </ProductContext.Provider>
    );
};
