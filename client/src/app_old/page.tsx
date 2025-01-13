"use client";

import '../styles.css';
import { useState } from 'react';
import Login from '../components/Login';
import Register from '../components/Register';

export default function Home() {
  const [showLogin, setShowLogin] = useState(true);

  return (
    <div className="container">
      <h1>Welcome to the Diet Management System</h1>
      <div className="form-toggle">
        <button onClick={() => setShowLogin(true)} className={showLogin ? 'active' : ''}>Login</button>
        <button onClick={() => setShowLogin(false)} className={!showLogin ? 'active' : ''}>Register</button>
      </div>
      <div className="form-container">
        {showLogin ? <Login /> : <Register />}
      </div>
    </div>
  );
}
