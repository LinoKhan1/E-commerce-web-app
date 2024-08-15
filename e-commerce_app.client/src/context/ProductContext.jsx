// Context/ProductContext.jsx
import React, { createContext, useContext, useState } from 'react';
import { fetchProducts, fetchProductById, fetchProductByCategory } from '../services/productService';

export const ProductContext = createContext();


export const ProductProvider = ({ children }) => {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(false);

    const getAllProducts = async () => {
        setLoading(true);
        try {
            const data = await fetchProducts();
            setProducts(data);
        } catch (error) {
            console.error('Failed to fetch products:', error);
        } finally {
            setLoading(false);
        }
    };

    const getProductById = async (id) => {
        try {
            return await fetchProductById(id);
        } catch (error) {
            console.error('Failed to fetch product:', error);
            throw error;
        }
    };

    const getProductsByCategory = async (categoryId) => {
        setLoading(true);
        try {
            const data = await fetchProductByCategory(categoryId);
            setProducts(data);
        } catch (error) {
            console.error('Failed to fetch products by category:', error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <ProductContext.Provider
            value={{
                products,
                loading,
                getAllProducts,
                getProductById,
                getProductsByCategory,
            }}
        >
            {children}
        </ProductContext.Provider>
    );
};
