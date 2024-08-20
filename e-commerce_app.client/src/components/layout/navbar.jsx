import React from 'react'
import { Link } from 'react-router-dom';
import CategoryLoader from '../../components/category/CategoryLoader.jsx';
import CategoryList from '../../components/category/CategoryList.jsx';
const Navbar = () => {

    return (
        <nav>
            <ul>
                <li>
                    <Link to="/">Home</Link>
                </li>
                <li>
                    <Link to="/shop">Shop</Link>
                    <CategoryLoader>
                        <CategoryList />
                    </CategoryLoader>
                </li>
                <li>
                    <Link to="/cart">Cart</Link>
                </li>
            </ul>
        </nav>
    );

}
export default Navbar;