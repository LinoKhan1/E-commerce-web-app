import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import useCategories from '../../hooks/useCategories.jsx';
import CategoryLoader from '../../components/category/CategoryLoader.jsx';
import { ProductProvider } from '../../context/ProductContext.jsx';
import ProductLoader from '../../components/product/ProductLoader.jsx';
import './Shop.scss';

function CategoryList() {
    const { categories, loading, error } = useCategories();

    if (loading) return <div>Loading categories...</div>;
    if (error) return <div>Error loading categories: {error.message}</div>;

    return (
        <ul className="category-list">
            {categories.map((category) => (
                <li key={category.id}>
                    <Link to={`/shop/category/${category.categoryId}`}>{category.name}</Link>
                </li>
            ))}
        </ul>
    );
}

const ShopPage = () => {
    const { categoryId } = useParams();
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
            </aside>

            <main className="shop-content">
                <div className="shop-header">
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

                <ProductProvider>
                    <ProductLoader categoryId={categoryId} />
                </ProductProvider>
            </main>
        </div>
    );
};

export default ShopPage;
