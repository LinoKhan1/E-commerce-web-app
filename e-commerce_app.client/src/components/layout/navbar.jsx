// src/components/layout/Navbar.jsx
import React from 'react'
import { Link, useNavigate } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';


const Navbar = () => {
    const { user, logout } = useAuth();  // Get the current user and logout function
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/');
    };
    return (
      
        <nav>
            <ul>
                <li>
                    <Link to="/">Home</Link>
                </li>
                <li>
                    <Link to="/shop">Shop</Link>
                </li>
                <li>
                    <Link to="/cart">Cart</Link>
                </li>

                {/* Conditional rendering based on user authentication status */}
                {user ? (
                    <>
                        <li>Welcome, {user.userName}</li>
                        <li>
                            <button onClick={handleLogout}>Logout</button>
                        </li>
                    </>
                ) : (
                    <>
                        <li>
                            <Link to="/login">Login</Link>
                        </li>
                        <li>
                            <Link to="/register">Register</Link>
                        </li>
                    </>
                )}
            </ul>
        </nav>
    );

}
export default Navbar;