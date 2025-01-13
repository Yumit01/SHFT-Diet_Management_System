import React, { useState, useEffect } from 'react';
import '../styles.css';

const ManageClients = () => {
  const [clients, setClients] = useState([]);
  const [currentClient, setCurrentClient] = useState({ name: '', email: '', age: '' });
  const [isUpdate, setIsUpdate] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);

  const fetchClients = async () => {
    try {
      const response = await fetch('https://localhost:7115/api/client');
      if (!response.ok) throw new Error('Network response was not ok');
      const data = await response.json();
      setClients(data);
    } catch (error) {
      console.error('Failed to fetch clients:', error);
    }
  };

  const openModal = (client = {}) => {
    setIsUpdate(!!client.id);
    setCurrentClient(client);
    setModalOpen(true);
  };

  const closeModal = () => {
    setModalOpen(false);
    setCurrentClient({ name: '', email: '', age: '' });
  };

  const handleSave = async () => {
    try {
      const method = isUpdate ? 'PUT' : 'POST';
      const url = `https://localhost:7115/api/client${isUpdate ? '/' + currentClient.id : ''}`;
      const response = await fetch(url, {
        method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(currentClient)
      });
      if (!response.ok) throw new Error('Network response was not ok');
      fetchClients();
      closeModal();
    } catch (error) {
      console.error('Failed to save client:', error);
    }
  };

  const handleDelete = async (id) => {
    try {
      const response = await fetch(`https://localhost:7115/api/client/${id}`, {
        method: 'DELETE'
      });
      if (!response.ok) throw new Error('Network response was not ok');
      fetchClients();
    } catch (error) {
      console.error('Failed to delete client:', error);
    }
  };

  useEffect(() => {
    fetchClients();
  }, []);

  return (
    <div className="manage-container">
      <header className="header">
        <h2>Manage Clients</h2>
        <button className="add-button" onClick={() => openModal()}>Add Client</button>
      </header>
      
      <table className="entity-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Age</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {clients.map((client) => (
            <tr key={client.id}>
              <td>{client.name}</td>
              <td>{client.email}</td>
              <td>{client.age}</td>
              <td>
                <button className="edit-button" onClick={() => openModal(client)}>Edit</button>
                <button className="delete-button" onClick={() => handleDelete(client.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {modalOpen && (
        <div className="modal">
          <div className="modal-content">
            <h3>{isUpdate ? 'Update Client' : 'Add Client'}</h3>
            <label>Name</label>
            <input
              type="text"
              value={currentClient.name || ''}
              onChange={(e) => setCurrentClient({ ...currentClient, name: e.target.value })}
            />
            <label>Email</label>
            <input
              type="email"
              value={currentClient.email || ''}
              onChange={(e) => setCurrentClient({ ...currentClient, email: e.target.value })}
            />
            <label>Age</label>
            <input
              type="number"
              value={currentClient.age || ''}
              onChange={(e) => setCurrentClient({ ...currentClient, age: e.target.value })}
            />
            <div className="modal-actions">
              <button onClick={closeModal}>Cancel</button>
              <button onClick={handleSave}>{isUpdate ? 'Update' : 'Add'}</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default ManageClients;
