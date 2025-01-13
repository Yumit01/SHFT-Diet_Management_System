# Diet Management System

## Project Overview
A web application to manage diet plans, clients, and dietitians.

## Installation Instructions
1. Clone the repository:
   ```sh
   git clone https://github.com/Yumit01/diet-management-system.git
   cd diet-management-system
   ```

2. Set up the backend:
   ```sh
   cd webAPI
   dotnet restore
   dotnet build
   ```

3. Set up the frontend:
   ```sh
   cd client
   npm install
   npm start
   ```

4. Run Docker containers:
   ```sh
   docker-compose up --build
   ```

## Directory Structure
```
diet-management-system/
├── client/
│   ├── src/
│   │   ├── components/
│   │   │   ├── ManageClients.js
│   │   │   └── ...
│   │   ├── App.js
│   │   └── ...
│   ├── public/
│   └── package.json
├── webAPI/
│   ├── Controllers/
│   │   ├── ClientController.cs
│   │   ├── DietitianController.cs
│   │   └── ...
│   ├── Models/
│   │   ├── ApplicationUser.cs
│   │   ├── Client.cs
│   │   ├── Dietitian.cs
│   │   └── ...
│   ├── Services/
│   │   ├── IUserService.cs
│   │   ├── UserService.cs
│   │   └── ...
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   └── ...
│   ├── Program.cs
│   ├── appsettings.json
│   └── ...
├── docker-compose.yml
└── README.md
```

## API Documentation
**Base URL:** `https://localhost:7115/api`

**Endpoints:**

- **GET /client**
  - Description: Get all clients
  - Response: `200 OK`
  - Response Body:
    ```json
    [
      {
        "id": 1,
        "name": "John Doe",
        "email": "john@example.com",
        "age": 30
      },
      ...
    ]
    ```

- **DELETE /client/{id}**
  - Description: Delete a client by ID
  - Response: `204 No Content`

## Frontend Documentation
**Components:**

- **ManageClients.js**
  - Description: Component to manage clients
  - State:
    - `clients`: List of clients
    - `currentClient`: Currently selected client
    - `isUpdate`: Boolean to indicate update mode
    - `modalOpen`: Boolean to control modal visibility
  - Methods:
    - `fetchClients`: Fetches clients from the API

## Backend Documentation
**Services:**

- **UserService.cs**
  - Methods:
    - `DeleteClient(int id)`: Deletes a client by ID
    - `DeleteDietitian(int id)`: Deletes a dietitian by ID
    - `UpdateDietitian(int id, Dietitian dietitian)`: Updates a dietitian

**Controllers:**

- **ClientController.cs**
  - Endpoints:
    - `DELETE /client/{id}`: Deletes a client by ID

## Database Schema
**Tables:**

- **Clients**
  - Columns:
    - `id`: INT, Primary Key
    - `name`: VARCHAR(255)
    - `email`: VARCHAR(255)
    - `age`: INT

- **Dietitians**
  - Columns:
    - `id`: INT, Primary Key
    - `name`: VARCHAR(255)
    - `email`: VARCHAR(255)
    - `phoneNumber`: VARCHAR(255)

## Running Tests
1. **Backend Tests:**
   ```sh
   cd webAPI
   dotnet test
   ```

2. **Frontend Tests:**
   ```sh
   cd client
   npm test
   ```

## Deployment Instructions
1. **Build Docker images:**
   ```sh
   docker-compose build
   ```

2. **Push Docker images to a registry:**
   ```sh
   docker push yumit01/diet-management-system-web
   docker push yumit01/diet-management-system-db
   ```

3. **Deploy using Docker Compose:**
   ```sh
   docker-compose up -d
   ```

This content provides a comprehensive overview of the project and detailed instructions for setup, usage, and deployment. Adjust the content as needed to fit your specific project requirements.
This content provides a comprehensive overview of the project and detailed instructions for setup, usage, and deployment. Adjust the content as needed to fit your specific project requirements.

## Contributing
1. Fork the repository
2. Create a new branch (`git checkout -b feature-branch`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature-branch`)
5. Open a pull request

## License
This project is licensed under the MIT License.