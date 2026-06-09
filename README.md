# ShagOx

ShagOx — marketplace platform for creating, searching, and managing advertisements.

The system allows users to publish listings, browse marketplace offers, manage their own ads, interact with sellers, and save favorites.

---

#  System Overview

ShagOx is a modular marketplace system built on a clean backend architecture with a focus on scalability, maintainability, and domain-driven design principles.

Core modules:
- User Management
- Advertisement Management
- Dictionary (Category, Currency, Condition)
- Specification System (dynamic attributes)
- Media Management (Images)
- Location System (City, Region)
- Role-Based Access Control (RBAC)

---

#  Architecture

- Client–Server architecture
- REST API
- JWT-based authentication
- Role-Based Access Control (RBAC)
- Modular monolith backend (ASP.NET Core)
- PostgreSQL as primary database

---

#  Domain Model

## Core Entities

- User
- Advertisement
- Category
- Currency
- Condition
- Image
- City / Region

## Key Concepts

- Advertisement is the core aggregate root
- Images are dependent entities (cascade delete)
- Category defines product type classification
- Currency defines pricing context
- Condition defines product state
- Properties field stores flexible JSONB attributes

---

#  Use Case Diagram

Use case diagram (draw.io):

- Primary diagram:
https://app.diagrams.net/#G17lcgQGeK2-osUXilmHRBSYLVNA8JiVTl#%7B%22pageId%22%3A%22E3P5FXf3VNNv1HI061pw%22%7D

---

#  Class Diagram

Class diagram (draw.io):

https://app.diagrams.net/#G1oVTzDGIShZ5trCL-5MSrwDHpb7I5TlZf#%7B%22pageId%22%3A%224rN7WS8nNAuwRa3o69Gu%22%7D

---

#  Development Guidelines

This project follows a defined set of development rules and conventions.

All standards for:
- commit messages
- pull requests
- branch naming
- code structure

are documented in a separate file:

`CONTRIBUTING.md`

Please refer to it before contributing to the project.

---

#  Features (Current Scope)

## Users
- Registration / Login (JWT)
- Profile management
- Create / edit / delete advertisements
- Browse marketplace listings
- Add advertisements to favorites

## Admin
- User management
- Advertisement moderation
- Reference data management (categories, currencies, conditions)

---

#  Tech Stack

## Backend
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Fluent API configurations

## Frontend
- React
- TypeScript

---

#  Database

- PostgreSQL
- JSONB used for flexible advertisement properties
- Indexed search fields for performance optimization

---