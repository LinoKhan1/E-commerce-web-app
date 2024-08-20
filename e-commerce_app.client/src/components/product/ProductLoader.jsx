import React, { useEffect } from 'react';
import useProducts from '../../hooks/useProduct';
import 'bootstrap/dist/css/bootstrap.min.css';
import './ProductGrid.scss';

const ProductLoader = ({ categoryId = null }) => {
    const { products, getAllProducts, getProductsByCategory, loading } = useProducts();

    useEffect(() => {
        if (categoryId) {
            getProductsByCategory(categoryId);
        } else {
            getAllProducts();
        }
    }, [categoryId]);

    if (loading) return <div>Loading products...</div>;

    return (
        <div className="row">
            {products.length > 0 ? (
                products.map((product) => (
                    <div key={product.id} className="col-lg-3 col-md-4 col-sm-6">
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
