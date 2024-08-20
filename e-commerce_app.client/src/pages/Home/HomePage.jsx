import React, { useState, useEffect } from 'react';
import useCategories from '../../hooks/useCategories';
import CategoryLoader from '../../components/category/CategoryLoader';
import CategorySection from '../../components/category/CategorySection';
import ProductLoader from '../../components/product/ProductLoader';
import { ProductProvider } from '../../context/ProductContext';
import { fetchLimitedProducts } from '../../services/productService';

import './Home.scss';
import 'bootstrap/dist/css/bootstrap.min.css';

const Home = () => {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const loadProducts = async () => {
            try {
                const data = await fetchLimitedProducts(8); // Fetch 8 products
                setProducts(data);
            } catch (error) {
                console.error('Error loading products:', error);
            } finally {
                setLoading(false);
            }
        };

        loadProducts();
    }, []);

    if (loading) return <div>Loading...</div>;

    return (
        <div className="home">
            {/* Hero Section */}
            <div className="hero-section">
                <section className="section">
                    <div className="row">
                        <div className="col">
                            <h1>Lorem ipsum dolor sit amet consectetur adipisicing elit. Nulla rem, facilis illo.</h1>
                            <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Voluptatem assumenda ut, ea quaerat omnis perspiciatis quam, doloribus incidunt.</p>
                            <button>Shop Now</button>
                        </div>
                        <div className="col">
                            <img src="" alt="Hero Image" />
                        </div>
                    </div>
                </section>
            </div>

            {/* Category Section */}
            <div className="category-section">
                <CategoryLoader>
                    <CategorySection />
                </CategoryLoader>
            </div>

            {/* Featured Product Section */}
            <div className="featured-product-section">
                <section className="section">
                    <div className="title">
                        <h1>Featured Product</h1>
                    </div>
                    <ProductProvider>
                        <div className="row">
                            <ProductLoader categoryId={3002} />
                        </div>
                    </ProductProvider>

                    <div className="row">
                        <div className="col">
                            <h1>Lorem Ipsum Dolor</h1>
                            <h2>From USD 000</h2>
                            <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Enim nobis vero quasi suscipit laudantium, tenetur minus error iure sit sint maiores, impedit deserunt alias animi ut quo sapiente fugiat vel.</p>
                            <button>Shop Now</button>
                        </div>
                        <div className="col">
                            <img src="" alt="Featured Product Image" />
                        </div>
                    </div>
                </section>
            </div>

            {/* Product Section */}
            <div className="product-section">
                <section className="section">
                    <div className="title">
                        <h1>Top Product</h1>
                    </div>
                    <div className="filter">
                        <div className="row">
                            <div className="col">
                                <ul>
                                    <li>lorem</li>
                                    <li>lorem</li>
                                    <li>lorem</li>
                                    <li>lorem</li>
                                    <li>lorem</li>
                                </ul>
                            </div>
                            <div className="col">
                                <button>Filter</button>
                            </div>
                        </div>
                    </div>

                    <div className="product-list">
                        {products.length === 0 ? (
                            <p>No products available</p>
                        ) : (
                            <div className="row">
                                {products.map(product => (
                                    <div key={product.id} className="col-lg-3 col-md-4 col-sm-6 mb-4">
                                        <div className="product-card">
                                            <img src={product.imageUrl} alt={product.name} className="img-fluid" />
                                            <h2>{product.name}</h2>
                                            <p>{product.description}</p>
                                            <p>${product.price.toFixed(2)}</p>
                                            <button>Add to Cart</button>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        )}
                    </div>
                    <button>Shop Now</button>
                </section>
            </div>

            {/* Discount Section */}
            <div className="discount-section">
                <section className="section">
                    <div className="row">
                        <div className="col">
                            <h1>DISCOUNTS</h1>
                            <p>Lorem Ipsum Dolor Sit</p>
                        </div>
                        <div className="col">
                            <img src="" alt="Discount Image" />
                        </div>
                    </div>
                </section>
            </div>

            {/* Resource Section */}
            <div className="resource-section">
                <section className="section">
                    <div className="title">
                        <h1>Latest News</h1>
                    </div>
                    <div className="row">
                        <div className="col">
                            <img src="" alt="News 1" />
                        </div>
                        <div className="col">
                            <img src="" alt="News 2" />
                        </div>
                        <div className="col">
                            <img src="" alt="News 3" />
                        </div>
                    </div>
                </section>
            </div>
        </div>
    );
};

export default Home;
