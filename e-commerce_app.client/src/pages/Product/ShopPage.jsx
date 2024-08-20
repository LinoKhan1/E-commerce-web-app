import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import useCategories from '../../hooks/useCategories.jsx';
import CategoryLoader from '../../components/category/CategoryLoader.jsx';
import { ProductProvider } from '../../context/ProductContext.jsx';
import ProductLoader from '../../components/product/ProductLoader.jsx';
import './Shop.scss'; // Ensure you have the appropriate styles

function CategoryList() {
    const { categoryId } = useParams(); // Get the category ID from the URL
    const { categories, loading, error } = useCategories();

    if (loading) return <div>Loading categories ...</div>;
    if (error) return <div>Error loading categories: {error.message}</div>;

    return (
        <ul className="category-list">
            {categories.map((category) => (
                <li key={category.id}>
                    <Link to={`/shop/category/${category.categoryId}`}>
                        {category.name}
                    </Link>
                </li>
            ))}
        </ul>
    );
}

const ShopPage = () => {
    const { categoryId } = useParams(); // Get the category ID from the URL
    console.log('categoryId:', categoryId);
    const [filters, setFilters] = useState({
        color: '',
        brand: '',
        priceRange: [0, 1000],
        rating: ''
    });
    const [sortOption, setSortOption] = useState('newest');

    return (
        <div className="shop-page">
            <aside className="shop-sidebar">
                <h3>Categories</h3>
                <CategoryLoader>
                    <CategoryList />
                </CategoryLoader>
                <h3>Shop by Color</h3>
                <div className="filter-section">
                    <label>
                        Color:
                        <select
                            value={filters.color}
                            onChange={(e) => setFilters({ ...filters, color: e.target.value })}
                        >
                            <option value="">All Colors</option>
                            <option value="red">Red</option>
                            <option value="blue">Blue</option>
                            <option value="green">Green</option>
                            {/* Add more colors as needed */}
                        </select>
                    </label>
                </div>

                <h3>Shop by Brand</h3>
                <div className="filter-section">
                    <label>
                        Brand:
                        <select
                            value={filters.brand}
                            onChange={(e) => setFilters({ ...filters, brand: e.target.value })}
                        >
                            <option value="">All Brands</option>
                            <option value="brand1">Brand 1</option>
                            <option value="brand2">Brand 2</option>
                            {/* Add more brands as needed */}
                        </select>
                    </label>
                </div>

                <h3>Shop by Price</h3>
                <div className="filter-section">
                    <label>
                        Price Range:
                        <input
                            type="range"
                            min="0"
                            max="1000"
                            value={filters.priceRange}
                            onChange={(e) => setFilters({ ...filters, priceRange: [e.target.value] })}
                        />
                    </label>
                </div>

                <h3>Shop by Rating</h3>
                <div className="filter-section">
                    <label>
                        Rating:
                        <select
                            value={filters.rating}
                            onChange={(e) => setFilters({ ...filters, rating: e.target.value })}
                        >
                            <option value="">All Ratings</option>
                            <option value="4">4 Stars & Up</option>
                            <option value="3">3 Stars & Up</option>
                            {/* Add more rating options */}
                        </select>
                    </label>
                </div>
            </aside>

            <main className="shop-content">
                <div className="shop-header">
                    <div className="items-found">
                        {/* Display the number of items found based on filters */}
                        {/* {products.length} items found */}
                    </div>
                    <div className="sort-options">
                        <label>
                            Sort by:
                            <select
                                value={sortOption}
                                onChange={(e) => setSortOption(e.target.value)}
                            >
                                <option value="newest">Newest</option>
                                <option value="top-rated">Top Rated</option>
                                <option value="low-to-high">Price: Low to High</option>
                                <option value="high-to-low">Price: High to Low</option>
                            </select>
                        </label>
                    </div>
                </div>
                <div className="product-grid">
                    <ProductProvider>
                        <ProductLoader categoryId={categoryId}/>
                    </ProductProvider>
                </div>
            </main>
        </div>
    );
};

export default ShopPage;
