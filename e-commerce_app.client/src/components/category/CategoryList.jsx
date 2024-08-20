import React from 'react';
import { Link } from 'react-router-dom';
import useCategories from '../../hooks/useCategories';

const CategoryList = () => {
    const { categories, loading, error } = useCategories();
    if (loading) return <div>loading categories ...</div>;
    if (error) return <div>Error loading categories: {error.message}</div>;


    return (
        <ul>
            {categories.map((category) => (
                <li key={category.id}>
                    <Link to={`/shop/category/${category.categoryId}`}>{category.name}</Link>
                    {console.log('Category ID:', category.categoryId, 'Category Name:', category.name)}
                </li>
            ))}
        </ul>
    );
};

export default CategoryList;