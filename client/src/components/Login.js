"use client";

import React, { useState, useEffect } from 'react';
import { useRouter } from 'next/router';

const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [message, setMessage] = useState('');
  const router = useRouter();

  const handleLogin = async () => {
    // Önceki hata mesajlarını temizleyelim
    setError('');
    setMessage('');

    // Email veya şifre girilmediyse uyarı verelim
    if (!email || !password) {
      setError('Please enter both email and password');
      setTimeout(() => {
        setError(''); // Hata mesajını 3 saniye sonra temizleyelim
      }, 3000);
      return;
    }

    try {
      const response = await fetch('https://localhost:7115/api/auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, password }),
      });

      console.log('Login Response:', response);

      if (response.ok) {
        const data = await response.json(); 
        console.log('Login successful!', data); 
        localStorage.setItem('role', data.role);
        
        setMessage('Login successful!');
        setTimeout(() => {
          router.push('/dashboard'); // 2 saniye sonra yönlendirme
        }, 2000);
        
      } else {
         const errorData = await response.json(); 
         setError('Login failed: ' + errorData.message);
         setTimeout(() => {
           setError(''); // Hata mesajını 3 saniye sonra temizleyelim
         }, 3000);
      } 
    } catch (error) { 
      setError('Login error: ' + error.message);
      setTimeout(() => {
        setError(''); // Hata mesajını 3 saniye sonra temizleyelim
      }, 3000);
    }
  };

  useEffect(() => {
    // Inputların değişiminde hata mesajlarını temizleyelim
    setError('');
  }, [email, password]);

  return (
    <div>
      <h2>Login</h2>
      {error && <p style={{ color: 'red' }}>{error}</p>} 
      {message && <p style={{ color: 'green' }}>{message}</p>} 
      <input 
        type="email" 
        value={email} 
        onChange={(e) => setEmail(e.target.value)} 
        placeholder="Enter your email" 
      />
      <input 
        type="password" 
        value={password} 
        onChange={(e) => setPassword(e.target.value)} 
        placeholder="Enter your password" 
      />
      <button onClick={handleLogin}>Login</button>
    </div>
  );
};

export default Login;
