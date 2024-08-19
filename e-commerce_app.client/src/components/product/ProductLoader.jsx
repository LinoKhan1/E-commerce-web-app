import React, { useEffect } from 'react';
import useProducts from '../../hooks/useProduct';
import Product_img from "../../assets/images/book.png";

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
       
            <div className="col-lg-3 col">
                {products.length > 0 ? (
                    products.map((product) => (
                        <div key={product.id}>
                            <img className="img-fluid" src={Product_img} alt="Product image" />
                            <h3>{product.name}</h3>
                            <p>Price: ${product.price}</p>
                        </div>
                    ))
                ) : (
                    <p>No products found.</p>
                )}
            </div>
        
        
    );
};
export default ProductLoader;
