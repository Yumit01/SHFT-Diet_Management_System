import React, { useState, useEffect } from 'react';

const ManageDietitians = () => {
  const [dietitians, setDietitians] = useState([]);
  const [currentDietitian, setCurrentDietitian] = useState({ name: "", email: "", age: "" });
  const [isUpdate, setIsUpdate] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);

  const fetchDietitians = async () => {
    try {
      const response = await fetch('https://localhost:7115/api/dietitian');
      if (!response.ok) throw new Error('Network response was not ok');
      const data = await response.json();
      setDietitians(data);
    } catch (error) {
      console.error('Failed to fetch dietitians:', error);
    }
  };

  const openModal = (dietitian = {}) => {
    setIsUpdate(!!dietitian.id);
    setCurrentDietitian(dietitian);
    setModalOpen(true);
  };

  const closeModal = () => {
    setModalOpen(false);
    setCurrentDietitian({ name: "", email: "", age: "" });
  };

  const handleSave = async () => {
    try {
      const method = isUpdate ? 'PUT' : 'POST';
      const url = `https://localhost:7115/api/dietitian${isUpdate ? '/' + currentDietitian.id : ''}`;
      const response = await fetch(url, {
        method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(currentDietitian)
      });
      if (!response.ok) throw new Error('Network response was not ok');
      fetchDietitians();
      closeModal();
    } catch (error) {
      console.error('Failed to save dietitian:', error);
    }
  };

  const handleDelete = async (id) => {
    try {
      const response = await fetch(`https://localhost:7115/api/dietitian/${id}`, {
        method: 'DELETE'
      });
      if (!response.ok) throw new Error('Network response was not ok');
      fetchDietitians();
    } catch (error) {
      console.error('Failed to delete dietitian:', error);
    }
  };

  useEffect(() => {
    fetchDietitians();
  }, []);

  return (
    <div className="manage-container">
      <header className="header">
        <h2>Manage Dietitians</h2>
        <button className="add-button" onClick={() => openModal()}>Add Dietitian</button>
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
          {dietitians.map((dietitian) => (
            <tr key={dietitian.id}>
              <td>{dietitian.name}</td>
              <td>{dietitian.email}</td>
              <td>{dietitian.age}</td>
              <td>
                <button className="edit-button" onClick={() => openModal(dietitian)}>Edit</button>
                <button className="delete-button" onClick={() => handleDelete(dietitian.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {modalOpen && (
        <div className="modal">
          <div className="modal-content">
            <h3>{isUpdate ? 'Update Dietitian' : 'Add Dietitian'}</h3>
            <label>Name</label>
            <input
              type="text"
              value={currentDietitian.name || ''}
              onChange={(e) => setCurrentDietitian({ ...currentDietitian, name: e.target.value })}
            />
            <label>Email</label>
            <input
              type="email"
              value={currentDietitian.email || ''}
              onChange={(e) => setCurrentDietitian({ ...currentDietitian, email: e.target.value })}
            />
            <label>Age</label>
            <input
              type="number"
              value={currentDietitian.age || ''}
              onChange={(e) => setCurrentDietitian({ ...currentDietitian, age: e.target.value })}
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

export default ManageDietitians;
