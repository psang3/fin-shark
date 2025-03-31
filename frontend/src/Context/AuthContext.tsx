import React, { createContext, useContext, useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';

interface AuthContextType {
    isAuthenticated: boolean;
    user: any | null;
    login: (token: string, userData: any) => void;
    logout: () => void;
    register: (username: string, email: string, password: string) => Promise<void>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
    const [user, setUser] = useState<any | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        // Check if user is logged in on component mount
        const token = localStorage.getItem('token');
        const userData = localStorage.getItem('user');
        if (token && userData) {
            setIsAuthenticated(true);
            setUser(JSON.parse(userData));
            // Set the default Authorization header for all future requests
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            // Navigate to search page if user is already authenticated
            navigate('/search');
        }
    }, [navigate]);

    const login = (token: string, userData: any) => {
        localStorage.setItem('token', token);
        localStorage.setItem('user', JSON.stringify(userData));
        setIsAuthenticated(true);
        setUser(userData);
        // Set the default Authorization header for all future requests
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        // Navigate to search page after successful login
        navigate('/search');
    };

    const logout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        setIsAuthenticated(false);
        setUser(null);
        // Remove the Authorization header
        delete axios.defaults.headers.common['Authorization'];
        navigate('/login');
    };

    const register = async (username: string, email: string, password: string) => {
        try {
            await axios.post('http://localhost:5097/api/account/register', {
                username,
                email,
                password
            });
            toast.success('Registration successful! Please login.');
            navigate('/login');
        } catch (error: any) {
            const message = error.response?.data?.message || 'Registration failed';
            toast.error(message);
            throw error;
        }
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, user, login, logout, register }}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthContext; 