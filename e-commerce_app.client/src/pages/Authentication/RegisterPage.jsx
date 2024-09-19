// src/pages/Auth/RegisterPage.jsx
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import useAuth from '../../hooks/useAuth';

const RegisterPage = () => {
    const { register, error } = useAuth(); // No need for user in register
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [userName, setUserName] = useState('');
    const [success, setSuccess] = useState(false); // New state for success message
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        await register({ userName, email, password });
        setSuccess(true); // Show success message when registration is successful
    };

    // Effect to handle redirect after successful registration
    useEffect(() => {
        if (success) {
            // Wait for 2 seconds, then redirect to login page
            setTimeout(() => {
                navigate('/login');
            }, 2000);
        }
    }, [success, navigate]);

    return (
        <div>
            <h2>Register</h2>
            {error && <p>{error}</p>}
            {success && <p>Registration successful! Redirecting to login...</p>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Username</label>
                    <input
                        type="text"
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Email</label>
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Password</label>
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Register</button>
            </form>
        </div>
    );
};

export default RegisterPage;
