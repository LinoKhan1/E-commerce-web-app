// src/services/productService.js
import * as productApi from '../api/productApi';

// fetch all products
export const fetchProducts = async (params) => {
  try {
    const response = await productApi.getProducts(params);
    // Process data if needed
    return response.data;
  } catch (error) {
    console.error('Error fetching products:', error);
    throw error;
  }
};

// fetch products by ID
export const fetchProductById = async (id) => {
  try {
    const response = await productApi.getProductById(id);
    return response.data;
  } catch (error) {
    console.error('Error fetching product:', error);
    throw error;
  }
};

/**
 * Fetch a limited number of products.
 * @param {number} limit - The number of products to fetch.
 * @returns {Promise<Object[]>} - Promise resolving to an array of products.
 */
export const fetchLimitedProducts = async (limit) => {
    try {
        const response = await productApi.getProducts({ limit });
        return response;
    } catch (error) {
        console.error('Failed to fetch limited products:', error);
        throw error;
    }
};

// Fetch products by category
export const fetchProductByCategory = async (categoryId) => {
    try {
        const products = await productApi.getProductsByCategory(categoryId);
        return products;
    } catch (error) {
        console.error(`Failed to fetch products by category with ID ${categoryId}:`, error);
        throw error;
    }
};


// Additional methods for creating, updating, and deleting products
