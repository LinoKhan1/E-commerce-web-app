import React, { useEffect } from 'react';
import useProducts from '../../hooks/useProduct';

const ProductLoader = ({ categoryId = null }) => {
    const { products, getAllProducts, getProductsByCategory, loading } = useProducts();

    useEffect(() => {
        if (categoryId) {
            console.log('Fetching products for category:', categoryId); // Debugging
            getProductsByCategory(categoryId);
        } else {
            console.log('Fetching all products'); // Debugging
            getAllProducts();
        }
    }, [categoryId]);

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
