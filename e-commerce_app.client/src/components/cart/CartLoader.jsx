import React, {useEffect} from "react";
import useCart from "../../hooks/useCart";

const CartLoader = () => {
    const { cartItems, loading, error, getCartItems } = useCart();

    useEffect(() => {
        getCartItems();
    }, [getCartItems]);

    if (loading) return <div>Loading cart items...</div>;
    if (error) return <div>Error loading cart items: {error.message}</div>;

    return (
        <div className="cart-items">
            {cartItems.length > 0 ? (
                cartItems.map(item => (
                    <div key={item.id} className="cart-item">
                        <h3>{item.productName}</h3>
                        <p>Quantity: {item.quantity}</p>
                        <p>Price: ${item.price}</p>
                    </div>
                ))
            ) : (
                <p>No items in the cart.</p>
            )}
        </div>
    );
};

export default CartLoader;