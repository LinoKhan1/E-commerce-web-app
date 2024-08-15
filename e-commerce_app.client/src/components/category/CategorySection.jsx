import React, { useState, useRef } from 'react';
import useCategories from '../../hooks/useCategories';
import CategoryLoader from '../../components/category/CategoryLoader';
import '../../pages/Home/Home.scss'; // Ensure your CSS file is correctly imported

const CategorySection = () => {
    const { categories, loading, error } = useCategories();

    if (loading) return <p>Loading categories...</p>;
    if (error) return <p>Error loading categories: {error.message}</p>;

    const scrollContainer = useRef(null);

    const scrollLeft = () => {
        scrollContainer.current.scrollBy({ left: -250, behavior: 'smooth' });
    };

    const scrollRight = () => {
        scrollContainer.current.scrollBy({ left: 250, behavior: 'smooth' });
    };

    return (
        <div className="scroller-container">
            <div className="scroller-header">
                <h1>Our Range</h1>
                <div className="scroller-buttons">
                    <button className="scroll-button" onClick={scrollLeft}>‹</button>
                    <button className="scroll-button" onClick={scrollRight}>›</button>
                </div>
            </div>
            <div className="scroll-wrapper row" ref={scrollContainer}>
                <div className="scroller-row">
                    {categories.map((category) => (
                        <div key={category.id} className="scroller-column">
                            <h3>{category.name}</h3>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default CategorySection;
