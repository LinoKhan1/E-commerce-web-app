import React, { useState, useEffect } from 'react'
import { useParams, Link } from 'react-router-dom';
import useCategories from '../../hooks/useCategories.jsx';
import CategoryLoader from '../../components/category/CategoryLoader.jsx';

function CategoryList() {
    const { categories, loading, error } = useCategories();
    if (loading) return <div>Loading categories ...</div>;
    if (error) return <div>Error loading categories: {error.message}</div>;

    return (
        
            <ul>
                {categories.map((category) => (
                    <li key={category.id}>
                        <Link to={`/shop/category/${category.id}`}>{category.name}</Link>
                    </li>
                ))}
            </ul>
        
    );
}


const Navbar = () => {
    return (
        <>
            <nav>
                <ul>
                    <li>
                        <Link to="/">Home</Link>

                    </li>
                    <li>
                        <Link to="/shop">Shop</Link>
                        <CategoryLoader>

                        <CategoryList/>
                        </CategoryLoader>
                    </li>
                    <li>
                        <Link to="/cart">Cart</Link>
                    </li>
                </ul>
            </nav>
        </>
    )
}
export default Navbar;