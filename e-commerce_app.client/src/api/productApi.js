import apiClient from './apiClient';

/**
 * Fetch all products.
 * @returns {Promise<Object[]>} - Promise resolving to an array of products.
 */
export const getProducts = async () => {
  try {
    const response = await apiClient.get('/product');
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
    const response = await apiClient.get(`/product/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching product with ID ${id}:`, error);
    throw error;
  }
};

/**
 * Fetch products from a specific category.
 * @param {number} categoryId - ID of the category.
 * @returns {Promise<Object[]>} - Promise resolving to an array of products.
 */
export const getProductsByCategory = async (categoryId) => {
  try {
    const response = await apiClient.get('/product/category', {
      params: { categoryId },
    });
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
    const response = await apiClient.post('/product', productData);
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
    const response = await apiClient.put(`/product/${id}`, productData);
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
    await apiClient.delete(`/products/${id}`);
  } catch (error) {
    console.error(`Error deleting product with ID ${id}:`, error);
    throw error;
  }
};
