# FinShark - Stock Portfolio Management Application

FinShark is a full-stack application for managing stock portfolios, built with React and .NET. It allows users to track stocks, manage portfolios, and analyze financial data.

## Features

- User authentication and authorization
- Stock search and tracking
- Portfolio management
- Real-time stock data
- Financial statements analysis
- Responsive design

## Tech Stack

### Frontend
- React
- TypeScript
- Tailwind CSS
- Axios
- React Router
- React Toastify

### Backend
- .NET 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- RESTful API

## Prerequisites

- Node.js (v14 or higher)
- .NET 8 SDK
- SQL Server
- npm or yarn

## Getting Started

1. Clone the repository
```bash
git clone https://github.com/yourusername/fin-shark.git
cd fin-shark
```

2. Set up the backend
```bash
cd api
dotnet restore
dotnet ef database update
dotnet run
```

3. Set up the frontend
```bash
cd frontend
npm install
npm start
```

4. Configure environment variables
- Create a `.env` file in the frontend directory
- Create `appsettings.json` in the api directory

## Project Structure

```
fin-shark/
├── frontend/           # React frontend application
│   ├── src/
│   ├── public/
│   └── package.json
│
└── api/               # .NET backend application
    ├── Controllers/
    ├── Models/
    ├── Services/
    └── Program.cs
```

## API Endpoints

- `POST /api/account/register` - User registration
- `POST /api/account/login` - User authentication
- `GET /api/portfolio` - Get user's portfolio
- `POST /api/portfolio` - Add stock to portfolio
- `DELETE /api/portfolio/{symbol}` - Remove stock from portfolio

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details 