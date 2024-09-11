import React, { useState, useEffect } from 'react';
import Navbar from "./Navbar";
import { Outlet } from "react-router-dom";
import GetAllCategories from "../Actions/CategoryActions";
import {CategoryDTO} from "../../Models/CategoryDTOs";


const Layout = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [categories, setCategories] = useState<CategoryDTO[] | undefined>(undefined);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [isError, setIsError] = useState<boolean>(false);

    // Fetch categories data using the custom hook
    const { categories: fetchedCategories, isLoading: fetchLoading, isError: fetchError } = GetAllCategories();

    const handleLogin = () => {
        setIsAuthenticated(true); // Update login status
    };

    useEffect(() => {
        if (isAuthenticated) {
            setIsLoading(true);
            // Update categories data when authenticated
            if (fetchedCategories) {
                setCategories(fetchedCategories);
                setIsLoading(false);
            }
            if (fetchError) {
                setIsError(true);
                setIsLoading(false);
            }
        }
    }, [isAuthenticated, fetchedCategories, fetchError]);

    if (isLoading) {
        return (
            <div className="flex justify-center items-center min-h-screen">
                {/* Loading spinner */}
                <div className="w-16 h-16 border-4 border-blue-500 border-dotted rounded-full animate-spin"></div>
            </div>
        );
    }

    if (isError) {
        return <p>Error loading categories.</p>;
    }

    return (
        <>
            <Navbar onLogin={handleLogin} categories={categories ?? []} />
            <main>
                <Outlet />
            </main>
        </>
    );
};

export default Layout;
