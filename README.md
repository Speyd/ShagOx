#  ShagOx

ShagOx — веб-платформа для размещения, поиска и управления объявлениями.  
Пользователи могут создавать объявления, просматривать предложения, добавлять в избранное и связываться с продавцами.

---

##  Features (MVP)

###  Users
- Регистрация и авторизация (JWT)
- Просмотр и поиск объявлений
- Создание и управление своими объявлениями
- Добавление в избранное
- Просмотр профилей пользователей

###  Admin
- Управление пользователями
- Управление объявлениями

---

##  Tech Stack

### Frontend
- React
- TypeScript

### Backend
- ASP.NET Core Web API
- JWT Authentication
- REST API

### Database
- PostgreSQL / MongoDB

---

##  Architecture

- Client–Server architecture
- REST API communication
- JWT-based authentication
- Role-based access control (RBAC)

---

##  Getting Started

### Prerequisites
- .NET 7+
- Node.js 18+
- npm

---

##  Backend

```bash
cd backend
dotnet restore
dotnet run
```

---

##  Frontend

```bash
cd frontend
npm install
npm run dev
```

---

##  Environment Variables

Backend `.env` example:

```env
JWT_SECRET=your_secret
DB_CONNECTION_STRING=your_connection_string
```

---

##  API (basic)

- POST /api/auth/register
- POST /api/auth/login
- GET /api/listings
- POST /api/listings

---

##  Project Structure

```bash
backend/
frontend/
```

---

##  Notes

- This is an initial MVP setup
- Project is under active development
