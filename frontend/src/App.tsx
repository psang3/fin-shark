import React from 'react';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Navbar from './Components/Navbar/Navbar';
import { AuthProvider } from './Context/AuthContext';

const App: React.FC = () => {
  return (
    <AuthProvider>
      <div className="min-h-screen bg-gray-100">
        <Navbar />
        <main className="container mx-auto px-4 py-8">
          <Outlet />
        </main>
        <ToastContainer position="bottom-right" />
      </div>
    </AuthProvider>
  );
};

export default App;