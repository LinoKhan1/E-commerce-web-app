import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchProductById } from '../../services/productService'; // Import from productService

const ProductDetail = () => {
    const { id } = useParams(); // Get the product ID from the URL parameters
    const [product, setProduct] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const getProductDetails = async () => {
            console.log('Product ID:', id); // Log the product ID
            try {
                const productData = await fetchProductById(id); // Use the service function to fetch product details
                console.log('Fetched Product Data:', productData); // Log the fetched product data
                if (!productData) {
                    throw new Error('No product found'); // Throw error if product data is null
                }
                setProduct(productData); // Set the product state
            } catch (err) {
                setError(err.message); // Handle any errors
            } finally {
                setLoading(false); // Stop loading
            }
        };

        getProductDetails();
    }, [id]);

    if (loading) return <div>Loading product details...</div>;
    if (error) return <div>Error fetching product details: {error}</div>;

    return (
        <div className="product-detail">
            <h1>{product.name}</h1>
            <p>{product.description}</p>
            <p className="product-price">${product.price.toFixed(2)}</p>
            <button className="btn btn-primary">Add to Cart</button>
        </div>
    );
};

export default ProductDetail;
