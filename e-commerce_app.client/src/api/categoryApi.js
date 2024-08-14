import apiClient from './apiClient';

/**
 * Fetch all categories from the API.
 * @returns {Promise<Object[]>} - Promise resolving to an array of categories.
 */
export const getCategories = async () => {
  try {
    const response = await apiClient.get('/category');
    return response.data;
  } catch (error) {
    console.error('Error fetching categories:', error);
    throw error;
  }
};
