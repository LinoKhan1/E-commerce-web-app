// src/components/ProductDetail.jsx
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchProductById } from '../../services/productService'; // Import from productService
import cartService from '../../services/cartService'; // Import the cart service

const ProductDetail = () => {
    const { id } = useParams(); // Get the product ID from the URL parameters
    const [product, setProduct] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [addingToCart, setAddingToCart] = useState(false); // State for managing adding to cart

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

    const handleAddToCart = async () => {
        setAddingToCart(true); // Set addingToCart to true while processing
        try {
            // Call the cart service to add the product to the cart
            await cartService.addToCart(product.id, 1); // Assuming you want to add 1 item
            alert("Product added to cart!"); // Notify user of success
        } catch (err) {
            console.error("Error adding product to cart:", err);
            alert("Failed to add product to cart. Please try again."); // Notify user of failure
        } finally {
            setAddingToCart(false); // Reset addingToCart state
        }
    };

    if (loading) return <div>Loading product details...</div>;
    if (error) return <div>Error fetching product details: {error}</div>;

    return (
        <div className="product-detail">
            <h1>{product.name}</h1>
            <p>{product.description}</p>
            <p className="product-price">${product.price.toFixed(2)}</p>
            <button
                className="btn btn-primary"
                onClick={handleAddToCart}
                disabled={addingToCart} // Disable button while adding to cart
            >
                {addingToCart ? 'Adding...' : 'Add To Cart'}
            </button>
        </div>
    );
};

export default ProductDetail;
