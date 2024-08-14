// src/services/productService.js
import * as productApi from '../api/productApi';

// src/services/productService.js
import * as productApi from '../api/productApi';

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

export const fetchProductById = async (id) => {
  try {
    const response = await productApi.getProductById(id);
    return response.data;
  } catch (error) {
    console.error('Error fetching product:', error);
    throw error;
  }
};

export const fetchProductByCategory = async (categoryId) => {
    try {
        const products = await productApi.getProductsByCategory(categoryId);
        return products;
    } catch (error) {
        console.error('Failed to fetch products by category', error);
        throw error;
    }
};

// Additional methods for creating, updating, and deleting products
