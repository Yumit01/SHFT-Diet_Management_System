"use client";

import React, { useState } from 'react';
import '../styles.css';

const Register = ({ setShowLogin }) => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [age, setAge] = useState('');
  const [role, setRole] = useState('Dietitian');
  const [message, setMessage] = useState('');
  const [errors, setErrors] = useState({});

  const validateForm = () => {
    const newErrors = {};
    if (!name) newErrors.name = 'Name is required';
    if (!email) newErrors.email = 'Email is required';
    if (!password) newErrors.password = 'Password is required';
    if (!phoneNumber) newErrors.phoneNumber = 'Phone number is required';
    if (!age || age < 18) newErrors.age = 'Age must be at least 18';
    return newErrors;
  };

  const handleRegister = async () => {
    const newErrors = validateForm();
    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      return;
    }

    try {
      const response = await fetch('https://localhost:7115/api/register', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          name,
          email,
          password,
          phoneNumber,
          age,
          role
        }),
      });

      if (response.ok) {
        setMessage('Registration successful! Redirecting to login...');
        setTimeout(() => {
          setShowLogin(true);
          setMessage('');
        }, 3000);
      } else {
        console.error('Registration failed');
        console.log(response);
      }
    } catch (error) {
      console.error('Registration error:', error);
    }
  };

  return (
    <div>
      <h2>Register</h2>
      {message && (
        <div className="notification">
          <p>{message}</p>
        </div>
      )}
      <form onSubmit={(e) => { e.preventDefault(); handleRegister(); }}>
        <div className="input-container">
          <input
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Enter your name"
            required
          />
          {errors.name && <span>{errors.name}</span>}
        </div>
        <div className="input-container">
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="Enter your email"
            required
          />
          {errors.email && <span>{errors.email}</span>}
        </div>
        <div className="input-container">
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Enter your password"
            required
          />
          {errors.password && <span>{errors.password}</span>}
        </div>
        <div className="input-container">
          <input
            type="text"
            value={phoneNumber}
            onChange={(e) => setPhoneNumber(e.target.value)}
            placeholder="Enter your phone number"
            required
          />
          {errors.phoneNumber && <span>{errors.phoneNumber}</span>}
        </div>
        <div className="input-container">
          <input
            type="number"
            value={age}
            onChange={(e) => setAge(e.target.value)}
            placeholder="Enter your age"
            required
          />
          {errors.age && <span>{errors.age}</span>}
        </div>
        <div className="input-container">
          <label htmlFor="role">Select your role:</label>
          <select
            id="role"
            value={role}
            onChange={(e) => setRole(e.target.value)}
          >
            <option value="Dietitian">Dietitian (Health professional)</option>
            <option value="Client">Client (Customer)</option>
          </select>
        </div>
        <button type="submit" className="register-button">Register</button>
      </form>
    </div>
  );
};

export default Register;
