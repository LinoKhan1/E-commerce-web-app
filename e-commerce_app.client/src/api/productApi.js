import apiClient from './apiClient';

/**
 * Fetch all products.
 * @returns {Promise<Object[]>} - Promise resolving to an array of products.
 */
export const getProducts = async (params = {}) => {
    try {
        const response = await apiClient.get('api/product', { params });
        return response.data;
    } catch (error) {
        console.error('Error fetching products:', error);
        throw error;
    }
};

/**
 * Fetch a single product by its ID.
 * @param {number} id - ID of the product.
 * @returns {Promise<Object>} - Promise resolving to the product data.
 */
export const getProductById = async (id) => {
    try {
        const response = await apiClient.get(`api/product/${id}`);
        if (response.status === 204) {
            return null; // Handle no content
        }
        return response.data; // Return the product data
    } catch (error) {
        console.error(`Error fetching product with ID ${id}:`, error);
        throw error; // Rethrow the error for further handling
    }
};


/**
 * Fetch products from a specific category.
 * @param {number} categoryId - ID of the category.
 * @returns {Promise<Object[]>} - Promise resolving to an array of products.
 */
export const getProductsByCategory = async (categoryId) => {
    try {
        const response = await apiClient.get(`/api/product/category/${categoryId}`);
        return response.data;
    } catch (error) {
        console.error(`Error fetching products from category ${categoryId}:`, error);
        throw error;
    }
};


/**
 * Create a new product.
 * @param {Object} productData - Data of the product to create.
 * @returns {Promise<Object>} - Promise resolving to the created product data.
 */
export const createProduct = async (productData) => {
  try {
    const response = await apiClient.post('api/product', productData);
    return response.data;
  } catch (error) {
    console.error('Error creating product:', error);
    throw error;
  }
};

/**
 * Update an existing product by its ID.
 * @param {number} id - ID of the product to update.
 * @param {Object} productData - Data to update the product with.
 * @returns {Promise<Object>} - Promise resolving to the updated product data.
 */
export const updateProduct = async (id, productData) => {
  try {
    const response = await apiClient.put(`api/product/${id}`, productData);
    return response.data;
  } catch (error) {
    console.error(`Error updating product with ID ${id}:`, error);
    throw error;
  }
};

/**
 * Delete a product by its ID.
 * @param {number} id - ID of the product to delete.
 * @returns {Promise<void>} - Promise resolving when the product is deleted.
 */
export const deleteProduct = async (id) => {
  try {
    await apiClient.delete(`api/product/${id}`);
  } catch (error) {
    console.error(`Error deleting product with ID ${id}:`, error);
    throw error;
  }
};
