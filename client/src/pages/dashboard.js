import React, { useEffect, useState } from 'react';
import { useRouter } from 'next/router';
import AdminDashboard from '../components/AdminDashboard';
import DietitianDashboard from '../components/DietitianDashboard';

const Dashboard = () => {
  const [userRole, setUserRole] = useState('');
  const router = useRouter();

  useEffect(() => {
    const role = localStorage.getItem('role');
    if (role) {
      setUserRole(role);
    } else {
      console.log('No role found, redirecting to login');
      router.push('/login');
    }
  }, []);

  return (
    <div>
      <h1>Welcome to the Dashboard</h1>
      <p>Your role is: {userRole}</p>
      {userRole === 'Admin' ? <AdminDashboard /> :
       userRole === 'Dietitian' ? <DietitianDashboard /> :
       <p>Role not found</p>}
    </div>
  );
};

export default Dashboard;
