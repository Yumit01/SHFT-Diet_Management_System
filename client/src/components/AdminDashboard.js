// AdminDashboard.jsx
import React, { useState } from 'react';
import '../styles.css';
import ManageClients from './ManageClients';
import ManageDietitians from './ManageDietitians';
import ManageDietPlans from './ManageDietPlans';

const AdminDashboard = () => {
  const [activeComponent, setActiveComponent] = useState('');

  const handleManageDietitians = () => setActiveComponent('dietitians');
  const handleManageClients = () => setActiveComponent('clients');
  const handleManageDietPlans = () => setActiveComponent('dietPlans');

  return (
    <div className="dashboard-container">
      <div className="sidebar">
        <h2>Admin Dashboard</h2>
        <ul>
          <li role="button" onClick={handleManageDietitians}>
            <i className="icon icon-dietitian"></i> Manage Dietitians
          </li>
          <li role="button" onClick={handleManageClients}>
            <i className="icon icon-client"></i> Manage Clients
          </li>
          <li role="button" onClick={handleManageDietPlans}>
            <i className="icon icon-diet-plan"></i> Manage Diet Plans
          </li>
        </ul>
      </div>
      <div className="content">
        {activeComponent === 'dietitians' && <ManageDietitians />}
        {activeComponent === 'clients' && <ManageClients />}
        {activeComponent === 'dietPlans' && <ManageDietPlans />}
      </div>
    </div>
  );
};

export default AdminDashboard;
