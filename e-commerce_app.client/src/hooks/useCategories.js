import { useContext } from 'react';
import { CategoryContext } from '../context/CategoryContext';

/**
 * Custom hook to use the category context.
 * @returns {Object} - The category context value.
 */
const useCategories = () => {
  return useContext(CategoryContext);
};

export default useCategories;
