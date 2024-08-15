// src/components/category/CategoryLoader.jsx


import { CategoryProvider } from "../../context/CategoryContext";


const CategoryLoader = ({ children }) => {
    return (
        <CategoryProvider>
            {children}
        </CategoryProvider>
    );
};

export default CategoryLoader;