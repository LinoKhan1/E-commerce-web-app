import React, { useEffect, useContext } from 'react';
import useProducts from '../../hooks/useProduct';
import { CartContext } from '../../context/CartContext'; // Adjust the import path as necessary

const ProductLoader = ({ categoryId = null }) => {
    const { products, getAllProducts, getProductsByCategory, loading } = useProducts();
    const { handleAddToCart } = useContext(CartContext); // Use handleAddToCart from CartContext

    useEffect(() => {
        if (categoryId) {
            console.log('Fetching products for category:', categoryId); // Debugging
            getProductsByCategory(categoryId);
        } else {
            console.log('Fetching all products'); // Debugging
            getAllProducts();
        }
    }, [categoryId]);

    const handleAddToCartClick = (product) => {
        if (handleAddToCart) {
            handleAddToCart(product);
        } else {
            console.error('handleAddToCart is not defined');
        }
    };

    if (loading) return <div>Loading products...</div>;

    return (
        <div className="row">
            {products && products.length > 0 ? (
                products.map((product) => (
                    <div key={product.id} className="col-lg-3">
                        <div className="product-card">
                            <img src={product.imageUrl} alt={product.name} className="product-image" />
                            <h3 className="product-name">{product.name}</h3>
                            <p className="product-price">${product.price.toFixed(2)}</p>
                            <button
                                onClick={() => handleAddToCartClick(product)}
                                className="btn btn-primary"
                            >
                                Add to Cart
                            </button>
                        </div>
                    </div>
                ))
            ) : (
                <p>No products found.</p>
            )}
        </div>
    );
};

export default ProductLoader;
