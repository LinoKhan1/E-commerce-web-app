import * as categoryApi from '../api/categoryApi';

/**
 * Fetch categories from the API.
 * @returns {Promise<Object[]>} - Promise resolving to an array of categories.
 */
export const fetchCategories = async () => {
  try {
    const categories = await categoryApi.getCategories();
    return categories;
  } catch (error) {
    console.error('Failed to fetch categories', error);
    throw error;
  }
};
