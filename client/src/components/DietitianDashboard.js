// DietitianDashboard.jsx
import React, { useState } from 'react';
import '../styles.css';
import ManageClients from '../components/ManageClients';
import ManageDietPlans from '../components/ManageDietPlans';

const DietitianDashboard = () => {
  const [activeComponent, setActiveComponent] = useState('');

  const handleManageClients = () => {
    setActiveComponent('clients');
  };

  const handleManageDietPlans = () => {
    setActiveComponent('dietPlans');
  };

  return (
    <div className="dashboard-container">
      <div className="sidebar">
        <h2>Dietitian Dashboard</h2>
        <ul>
          <li role="button" onClick={handleManageClients}>
            <i className="icon icon-client"></i> Manage Clients
          </li>
          <li role="button" onClick={handleManageDietPlans}>
            <i className="icon icon-diet-plan"></i> Manage Diet Plans
          </li>
        </ul>
      </div>
      <div className="content">
        {activeComponent === 'clients' && <ManageClients />}
        {activeComponent === 'dietPlans' && <ManageDietPlans />}
      </div>
    </div>
  );
};

export default DietitianDashboard;
